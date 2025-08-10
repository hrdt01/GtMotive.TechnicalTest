using GtMotive.Estimate.Microservice.Domain.DTO;
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
        private readonly ILogger<FleetService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetService"/> class.
        /// </summary>
        /// <param name="fleetRepository">Instance of <see cref="IFleetRepository"/>.</param>
        /// <param name="logger">Instance of <see cref="ILogger"/>.</param>
        public FleetService(
            IFleetRepository fleetRepository,
            ILogger<FleetService> logger)
        {
            ArgumentNullException.ThrowIfNull(fleetRepository);
            ArgumentNullException.ThrowIfNull(logger);

            _fleetRepository = fleetRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<FleetDto?> AddNewFleet(string newFleetName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(newFleetName);

            var fleetDto = new FleetDto { FleetName = newFleetName };

            return await _fleetRepository.AddNewFleet(fleetDto);
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

            return await _fleetRepository.AddNewVehicle(existingFleet.FleetId, sourceVehicle);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<VehicleDto>> GetAvailableFleetVehicles(FleetDto sourceFleet)
        {
            ArgumentNullException.ThrowIfNull(sourceFleet);

            return await _fleetRepository.GetAvailableFleetVehicles(sourceFleet.FleetId);
        }
    }
}
