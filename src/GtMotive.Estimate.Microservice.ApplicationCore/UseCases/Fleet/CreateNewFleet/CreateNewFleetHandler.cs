using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Fleet.CreateNewFleet
{
    /// <inheritdoc />
    public class CreateNewFleetHandler : IRequestHandler<CreateNewFleetRequest, CreateNewFleetResponse>
    {
        private readonly IFleetService _fleetService;
        private readonly ILogger<CreateNewFleetHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewFleetHandler"/> class.
        /// </summary>
        /// <param name="fleetService"><see cref="IFleetService"/> instance.</param>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        public CreateNewFleetHandler(
            IFleetService fleetService,
            ILogger<CreateNewFleetHandler> logger)
        {
            ArgumentNullException.ThrowIfNull(fleetService);
            ArgumentNullException.ThrowIfNull(logger);

            _fleetService = fleetService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<CreateNewFleetResponse> Handle(CreateNewFleetRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            _logger.LogInfoHandlingGetRequest(
                $"{nameof(CreateNewFleetHandler)} - {nameof(Handle)} - ",
                request.FleetName);
            try
            {
                var response = await _fleetService.AddNewFleet(request.FleetName);
                var returnedResponse = new CreateNewFleetResponse { Fleet = response };
                return returnedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogErrorProcessingRequest(
                    $"{nameof(CreateNewFleetHandler)} - {nameof(Handle)} - ", ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
