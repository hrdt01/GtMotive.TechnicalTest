namespace GtMotive.Estimate.Microservice.Infrastructure.Database.DbEntities
{
    /// <summary>
    /// Entity FleetVehicle.
    /// </summary>
    public class FleetVehicle
    {
        /// <summary>
        /// Gets or sets the fleet vehicle id.
        /// </summary>
        public Guid FleetVehicleId { get; set; }

        /// <summary>
        /// Gets or sets the fleet id.
        /// </summary>
        public Guid FleetId { get; set; }

        /// <summary>
        /// Gets the fleet id.
        /// </summary>
        public Fleet Fleet { get; }

        /// <summary>
        /// Gets or sets the vehicle id.
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets the vehicle.
        /// </summary>
        public Vehicle Vehicle { get; }
    }
}
