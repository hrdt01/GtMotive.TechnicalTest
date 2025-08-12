using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Customer.CreateNewCustomer
{
    /// <inheritdoc />
    internal sealed class CreateNewCustomerHandler : IRequestHandler<CreateNewCustomerRequest, CreateNewCustomerResponse>
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CreateNewCustomerHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewCustomerHandler"/> class.
        /// </summary>
        /// <param name="customerService"><see cref="ICustomerService"/> instance.</param>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        public CreateNewCustomerHandler(
            ICustomerService customerService,
            ILogger<CreateNewCustomerHandler> logger)
        {
            ArgumentNullException.ThrowIfNull(customerService);
            ArgumentNullException.ThrowIfNull(logger);

            _customerService = customerService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<CreateNewCustomerResponse> Handle(CreateNewCustomerRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            _logger.LogInfoHandlingGetRequest(
                $"{nameof(CreateNewCustomerHandler)} - {nameof(Handle)} - ",
                request.CustomerName);
            try
            {
                var response = await _customerService.AddNewCustomer(request.CustomerName);
                var returnedResponse = new CreateNewCustomerResponse { Customer = response };
                return returnedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogErrorProcessingRequest(
                    $"{nameof(CreateNewCustomerHandler)} - {nameof(Handle)} - ", ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
