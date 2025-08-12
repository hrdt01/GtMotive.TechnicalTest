using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Fleet.AddNewVehicleToFleet
{
    /// <inheritdoc />
    internal sealed class AddNewVehicleToFleetHandler
        : IRequestHandler<AddNewVehicleToFleetRequest, AddNewVehicleToFleetResponse>
    {
        private readonly IFleetService _fleetService;
        private readonly ILogger<AddNewVehicleToFleetHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddNewVehicleToFleetHandler"/> class.
        /// </summary>
        /// <param name="fleetService"><see cref="IFleetService"/> instance.</param>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        public AddNewVehicleToFleetHandler(
            IFleetService fleetService,
            ILogger<AddNewVehicleToFleetHandler> logger)
        {
            ArgumentNullException.ThrowIfNull(fleetService);
            ArgumentNullException.ThrowIfNull(logger);

            _fleetService = fleetService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<AddNewVehicleToFleetResponse> Handle(AddNewVehicleToFleetRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            _logger.LogInfoHandlingGetRequest(
                $"{nameof(AddNewVehicleToFleetHandler)} - {nameof(Handle)} - ",
                request.FleetId);
            try
            {
                var sourceFleet = new FleetDto { FleetId = Guid.Parse(request.FleetId) };
                var sourceVehicle = new VehicleDto
                {
                    Brand = request.VehicleBrand,
                    Model = request.VehicleModel,
                    ManufacturedOn = request.VehicleManufacturedOn
                };
                var response = await _fleetService.AddNewVehicle(sourceFleet, sourceVehicle);
                var returnedResponse = new AddNewVehicleToFleetResponse { Fleet = response };
                return returnedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogErrorProcessingRequest(
                    $"{nameof(AddNewVehicleToFleetHandler)} - {nameof(Handle)} - ", ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
