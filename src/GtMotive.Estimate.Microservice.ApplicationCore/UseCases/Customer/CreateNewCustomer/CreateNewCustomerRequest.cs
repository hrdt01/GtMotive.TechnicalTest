using MediatR;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Customer.CreateNewCustomer
{
    /// <inheritdoc />
    public class CreateNewCustomerRequest : IRequest<CreateNewCustomerResponse>
    {
        /// <summary>
        /// Gets or sets the Customer name.
        /// </summary>
        public string CustomerName { get; set; } = null!;
    }
}
