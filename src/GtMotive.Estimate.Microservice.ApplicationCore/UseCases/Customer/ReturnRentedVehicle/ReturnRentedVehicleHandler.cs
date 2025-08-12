using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Customer.ReturnRentedVehicle
{
    /// <inheritdoc />
    internal sealed class ReturnRentedVehicleHandler
        : IRequestHandler<ReturnRentedVehicleRequest, ReturnRentedVehicleResponse>
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<ReturnRentedVehicleHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnRentedVehicleHandler"/> class.
        /// </summary>
        /// <param name="customerService"><see cref="ICustomerService"/> instance.</param>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        public ReturnRentedVehicleHandler(
            ICustomerService customerService,
            ILogger<ReturnRentedVehicleHandler> logger)
        {
            ArgumentNullException.ThrowIfNull(customerService);
            ArgumentNullException.ThrowIfNull(logger);

            _customerService = customerService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<ReturnRentedVehicleResponse> Handle(ReturnRentedVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            _logger.LogInfoHandlingGetRequest(
                $"{nameof(ReturnRentedVehicleHandler)} - {nameof(Handle)} - ",
                request.RentedVehicleId);
            try
            {
                var sourceRentedVehicle = new RentedVehicleDto
                {
                    RentedVehicleId = Guid.Parse(request.RentedVehicleId),
                    CustomerId = Guid.Parse(request.CustomerId)
                };

                var response = await _customerService.ReturnRentedVehicle(sourceRentedVehicle);
                var returnedResponse = new ReturnRentedVehicleResponse { RentedVehicle = response };
                return returnedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogErrorProcessingRequest(
                    $"{nameof(ReturnRentedVehicleHandler)} - {nameof(Handle)} - ", ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
