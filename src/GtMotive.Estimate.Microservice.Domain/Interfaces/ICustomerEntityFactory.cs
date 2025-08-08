using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// ICustomerEntityFactory definition.
    /// </summary>
    public interface ICustomerEntityFactory
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ICustomer"/> class.
        /// </summary>
        /// <param name="customerName">Customer name.</param>
        /// <returns>Instance of <see cref="ICustomer"/>.</returns>
        ICustomer NewCustomer(CustomerName customerName);
    }
}
