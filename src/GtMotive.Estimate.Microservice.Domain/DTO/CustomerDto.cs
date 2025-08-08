namespace GtMotive.Estimate.Microservice.Domain.DTO
{
    /// <summary>
    /// DTO Entity Customer.
    /// </summary>
    public class CustomerDto : IDtoEntity
    {
        /// <summary>
        /// Gets or sets CustomerId.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets Customer Name.
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// Gets or sets RentedVehicles.
        /// </summary>
        public IEnumerable<RentedVehicleDto>? RentedVehicles { get; set; }
    }
}
