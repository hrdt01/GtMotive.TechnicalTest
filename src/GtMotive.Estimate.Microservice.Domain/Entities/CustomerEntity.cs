using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <inheritdoc />
    public class CustomerEntity : Customer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerEntity"/> class.
        /// </summary>
        /// <param name="customerName">Instance of <see cref="CustomerName"/> struct.</param>
        public CustomerEntity(CustomerName customerName)
        {
            CustomerName = customerName;
            Id = new CustomerId(Guid.NewGuid());
            RentedVehicles = new RentedVehicles(Enumerable.Empty<IRentedVehicle>().ToList());
        }
    }
}
