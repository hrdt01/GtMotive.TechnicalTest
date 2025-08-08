using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GtMotive.Estimate.Microservice.Services.Implementation
{
    /// <inheritdoc />
    public class FleetService : IFleetService
    {
        private readonly IFleetRepository _fleetRepository;
        private readonly IFleetEntityFactory _fleetEntityFactory;
        private readonly IVehicleEntityFactory _vehicleEntityFactory;
        private readonly ILogger<FleetService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetService"/> class.
        /// </summary>
        /// <param name="fleetRepository">Instance of <see cref="IFleetRepository"/>.</param>
        /// <param name="fleetEntityFactory">Instance of <see cref="IFleetEntityFactory"/>.</param>
        /// <param name="vehicleEntityFactory">Instance of <see cref="IVehicleEntityFactory"/>.</param>
        /// <param name="logger">Instance of <see cref="ILogger"/>.</param>
        public FleetService(
            IFleetRepository fleetRepository,
            IFleetEntityFactory fleetEntityFactory,
            IVehicleEntityFactory vehicleEntityFactory,
            ILogger<FleetService> logger)
        {
            ArgumentNullException.ThrowIfNull(fleetRepository);
            ArgumentNullException.ThrowIfNull(fleetEntityFactory);
            ArgumentNullException.ThrowIfNull(vehicleEntityFactory);
            ArgumentNullException.ThrowIfNull(logger);

            _fleetRepository = fleetRepository;
            _fleetEntityFactory = fleetEntityFactory;
            _vehicleEntityFactory = vehicleEntityFactory;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<FleetDto?> AddNewFleet(string newFleetName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(newFleetName);

            var entity = _fleetEntityFactory.NewFleet(new FleetName(newFleetName));

            return await _fleetRepository.AddNewFleet(entity);
        }

        /// <inheritdoc />
        public async Task<FleetDto?> AddNewVehicle(FleetDto sourceFleet, VehicleDto sourceVehicle)
        {
            ArgumentNullException.ThrowIfNull(sourceFleet);
            ArgumentNullException.ThrowIfNull(sourceVehicle);

            var existingFleet = await _fleetRepository.GetFleetById(sourceFleet.FleetId);
            if (existingFleet == null)
            {
                _logger.LogWarningNotFoundFleet(
                    $"{nameof(FleetService)} - {nameof(AddNewVehicle)} - ",
                    sourceFleet.FleetId.ToString());

                ArgumentNullException.ThrowIfNull(existingFleet);
            }

            var entity = _vehicleEntityFactory.NewVehicle(
                new Brand(sourceVehicle.Brand!),
                new Model(sourceVehicle.Model!),
                new ManufacturedOn(sourceVehicle.ManufacturedOn));

            return await _fleetRepository.AddNewVehicle(existingFleet.FleetId, entity);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<VehicleDto>> GetAvailableFleetVehicles(FleetDto sourceFleet)
        {
            ArgumentNullException.ThrowIfNull(sourceFleet);

            return await _fleetRepository.GetAvailableFleetVehicles(sourceFleet.FleetId);
        }
    }
}
