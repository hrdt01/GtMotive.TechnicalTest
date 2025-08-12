using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Customer.RentVehicle
{
    /// <inheritdoc />
    internal sealed class RentVehicleHandler
        : IRequestHandler<RentVehicleRequest, RentVehicleResponse>
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<RentVehicleHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleHandler"/> class.
        /// </summary>
        /// <param name="customerService"><see cref="ICustomerService"/> instance.</param>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        public RentVehicleHandler(
            ICustomerService customerService,
            ILogger<RentVehicleHandler> logger)
        {
            ArgumentNullException.ThrowIfNull(customerService);
            ArgumentNullException.ThrowIfNull(logger);

            _customerService = customerService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<RentVehicleResponse> Handle(RentVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            _logger.LogInfoHandlingGetRequest(
                $"{nameof(RentVehicleHandler)} - {nameof(Handle)} - ",
                $"CustomerId: {request.CustomerId} VehicleId: {request.VehicleId}");
            try
            {
                var sourceRentedVehicle = new RentedVehicleDto
                {
                    FleetId = Guid.Parse(request.FleetId),
                    VehicleId = Guid.Parse(request.VehicleId),
                    CustomerId = Guid.Parse(request.CustomerId),
                    StartRent = request.StartRent,
                    EndRent = request.EndRent
                };

                var response = await _customerService.RentVehicle(sourceRentedVehicle);
                var returnedResponse = new RentVehicleResponse { RentedVehicle = response };
                return returnedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogErrorProcessingRequest(
                    $"{nameof(RentVehicleHandler)} - {nameof(Handle)} - ", ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
