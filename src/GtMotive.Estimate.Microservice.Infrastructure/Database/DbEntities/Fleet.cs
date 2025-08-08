namespace GtMotive.Estimate.Microservice.Infrastructure.Database.DbEntities
{
    /// <summary>
    /// Db Entity Fleet.
    /// </summary>
    public class Fleet
    {
        /// <summary>
        /// Gets or sets the fleet id.
        /// </summary>
        public Guid FleetId { get; set; }

        /// <summary>
        /// Gets or sets the fleet name.
        /// </summary>
        public required string FleetName { get; set; }

        /// <summary>
        /// Gets the fleet vehicles.
        /// </summary>
        public ICollection<FleetVehicle>? FleetVehicles { get; }
    }
}
