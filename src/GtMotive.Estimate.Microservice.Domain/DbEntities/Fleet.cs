namespace GtMotive.Estimate.Microservice.Domain.DbEntities
{
    /// <summary>
    /// Db Entity Fleet.
    /// </summary>
    public class Fleet : IDbEntity
    {
        /// <summary>
        /// Gets or sets the fleet id.
        /// </summary>
        public Guid FleetId { get; set; }

        /// <summary>
        /// Gets or sets the fleet name.
        /// </summary>
        public string FleetName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the fleet vehicles.
        /// </summary>
        public IEnumerable<FleetVehicle>? FleetVehicles { get; set; }
    }
}
