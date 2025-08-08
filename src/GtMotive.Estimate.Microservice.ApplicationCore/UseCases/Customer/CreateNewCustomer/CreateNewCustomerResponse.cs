using GtMotive.Estimate.Microservice.Domain.DTO;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Customer.CreateNewCustomer
{
    /// <summary>
    /// CreateNewCustomerResponse definition.
    /// </summary>
    public class CreateNewCustomerResponse
    {
        /// <summary>
        /// Gets or sets the Customer.
        /// </summary>
        public CustomerDto? Customer { get; set; }
    }
}
