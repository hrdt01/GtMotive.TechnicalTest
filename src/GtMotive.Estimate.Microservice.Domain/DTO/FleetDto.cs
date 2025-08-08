namespace GtMotive.Estimate.Microservice.Domain.DTO
{
    /// <summary>
    /// DTO Entity Fleet.
    /// </summary>
    public class FleetDto : IDtoEntity
    {
        /// <summary>
        /// Gets or sets FleetId.
        /// </summary>
        public Guid FleetId { get; set; }

        /// <summary>
        /// Gets or sets Fleet Name.
        /// </summary>
        public string FleetName { get; set; } = null!;

        /// <summary>
        /// Gets or sets Vehicles.
        /// </summary>
        public IEnumerable<VehicleDto>? Vehicles { get; set; }
    }
}
