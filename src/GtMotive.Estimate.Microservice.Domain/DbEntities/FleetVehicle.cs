namespace GtMotive.Estimate.Microservice.Domain.DbEntities
{
    /// <summary>
    /// Entity FleetVehicle.
    /// </summary>
    public class FleetVehicle : IDbEntity
    {
        /// <summary>
        /// Gets or sets the fleet vehicle id.
        /// </summary>
        public Guid FleetVehicleId { get; protected set; }

        /// <summary>
        /// Gets or sets the fleet id.
        /// </summary>
        public Guid FleetId { get; set; }

        /// <summary>
        /// Gets the fleet id.
        /// </summary>
        public Fleet? Fleet { get; }

        /// <summary>
        /// Gets or sets the vehicle id.
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the vehicle.
        /// </summary>
        public Vehicle? Vehicle { get; set; }
    }
}
