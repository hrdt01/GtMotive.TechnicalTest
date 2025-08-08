namespace GtMotive.Estimate.Microservice.Domain.DbEntities
{
    /// <summary>
    /// Db Entity Customer.
    /// </summary>
    public class Customer : IDbEntity
    {
        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        public string? CustomerName { get; set; }
    }
}
