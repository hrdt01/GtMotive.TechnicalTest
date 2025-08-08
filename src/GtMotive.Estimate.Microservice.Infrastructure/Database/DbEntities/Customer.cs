namespace GtMotive.Estimate.Microservice.Infrastructure.Database.DbEntities
{
    /// <summary>
    /// Entity Customer.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        public required string CustomerName { get; set; }

        /// <summary>
        /// Gets the rented vehicles.
        /// </summary>
        public ICollection<RentedVehicle>? RentedVehicles { get; }
    }
}
