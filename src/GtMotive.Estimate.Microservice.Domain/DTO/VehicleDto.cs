namespace GtMotive.Estimate.Microservice.Domain.DTO
{
    /// <summary>
    /// DTO Entity Vehicle.
    /// </summary>
    public class VehicleDto : IDtoEntity
    {
        /// <summary>
        /// Gets or sets VehicleId.
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets Brand.
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// Gets or sets Model.
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// Gets or sets ManufacturedOn.
        /// </summary>
        public DateTime ManufacturedOn { get; set; }
    }
}
