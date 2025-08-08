namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value object for Fleet's identifier.
    /// </summary>
    public readonly struct FleetId
    {
        private readonly Guid _fleetId;

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetId"/> struct.
        /// </summary>
        public FleetId()
        {
            _fleetId = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetId"/> struct.
        /// </summary>
        /// <param name="fleetId">Fleet identifier.</param>
        public FleetId(Guid fleetId)
        {
            _fleetId = fleetId;
        }

        /// <summary>
        /// Override method.
        /// </summary>
        /// <returns>Fleet's identifier.</returns>
        public override string ToString()
        {
            return _fleetId.ToString();
        }
    }
}
