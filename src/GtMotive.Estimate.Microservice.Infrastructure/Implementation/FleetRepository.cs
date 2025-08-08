using System.Collections.ObjectModel;
using GtMotive.Estimate.Microservice.Domain.DbEntities;
using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Database;
using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Implementation
{
    /// <inheritdoc />
    public class FleetRepository : IFleetRepository
    {
        private readonly FleetContext _fleetContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetRepository"/> class.
        /// </summary>
        /// <param name="fleetContext">DB context.</param>
        public FleetRepository(FleetContext fleetContext)
        {
            ArgumentNullException.ThrowIfNull(fleetContext);

            _fleetContext = fleetContext;
            _fleetContext.Database.EnsureCreated();
        }

        /// <inheritdoc />
        public async Task<FleetDto?> AddNewVehicle(Guid fleetId, IVehicle sourceVehicle)
        {
            ArgumentNullException.ThrowIfNull(fleetId);
            ArgumentNullException.ThrowIfNull(sourceVehicle);

            var fleetFromDb = await GetFleetById(fleetId);
            if (fleetFromDb == null)
            {
                ArgumentNullException.ThrowIfNull(fleetFromDb);
            }

            var newVehicleDbInstance = sourceVehicle.ToDbEntity();
            _ = await _fleetContext.Vehicles.AddAsync(newVehicleDbInstance);

            var newFleetVehicle = new FleetVehicle { FleetId = fleetFromDb.FleetId, VehicleId = newVehicleDbInstance.VehicleId };
            _ = await _fleetContext.FleetVehicles.AddAsync(newFleetVehicle);

            await _fleetContext.SaveChangesAsync();

            return await GetFleetById(fleetId);
        }

        /// <inheritdoc />
        public async Task<FleetDto?> AddNewFleet(IFleet newFleet)
        {
            ArgumentNullException.ThrowIfNull(newFleet);

            var fleetDbInstance = newFleet.ToDbEntity();
            await _fleetContext.Fleet.AddAsync(fleetDbInstance);
            await _fleetContext.SaveChangesAsync();

            return await GetFleetById(fleetDbInstance.FleetId);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<VehicleDto>> GetAvailableFleetVehicles(Guid fleetId)
        {
            ArgumentNullException.ThrowIfNull(fleetId);

            var fleetFromDb = await GetFleetById(fleetId);
            if (fleetFromDb == null)
            {
                ArgumentNullException.ThrowIfNull(fleetFromDb);
            }

            // Querying rented vehicles that currently has a rent start date
            // and with rent end date defined but not accomplished yet
            var currentNow = DateTime.UtcNow;
            var currentRented =
                _fleetContext.RentedVehicles.AsNoTracking()
                    .Where(vehicle => vehicle.RentFinishedOn > currentNow)
                    .Select(vehicle => vehicle.VehicleId);

            // From fleet, only are available those vehicles whose vehicle id is not present in the previous collection
            var subset =
                fleetFromDb.Vehicles?.Where(vehicle => !currentRented.Contains(vehicle.VehicleId)).ToArray();
            return subset != null
                ? new ReadOnlyCollection<VehicleDto>(subset)
                : ReadOnlyCollection<VehicleDto>.Empty;
        }

        /// <inheritdoc />
        public async Task<FleetDto?> GetFleetById(Guid fleetId)
        {
            ArgumentNullException.ThrowIfNull(fleetId);

            var fromDb = await _fleetContext.Fleet.AsNoTracking()
                .OrderBy(fleet => fleet.FleetId)
                .Where(fleet => fleet.FleetId == fleetId)
                .Include(fleet => fleet.FleetVehicles)
                .FirstOrDefaultAsync();

            if (fromDb == null)
            {
                return null;
            }

            var vehicleIdCollection = fromDb.FleetVehicles?.Select(fleetVehicle => fleetVehicle.VehicleId);
            var fleetVehicles = Enumerable.Empty<Vehicle>();

            if (vehicleIdCollection != null)
            {
                fleetVehicles = await _fleetContext.Vehicles.AsNoTracking()
                    .Where(vehicle => vehicleIdCollection.Contains(vehicle.VehicleId))
                    .ToListAsync();
            }

            foreach (var fleetVehicle in fromDb.FleetVehicles!)
            {
                fleetVehicle.Vehicle =
                    fleetVehicles.FirstOrDefault(vehicle => vehicle.VehicleId == fleetVehicle.VehicleId);
            }

            return new FleetDto
            {
                FleetId = fromDb.FleetId,
                FleetName = fromDb.FleetName,
                Vehicles = fromDb.FleetVehicles.FromDbEntityToDto()
            };
        }
    }
}
