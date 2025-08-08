namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value object for Fleet's identifier.
    /// </summary>
    public readonly struct FleetName
    {
        private readonly string _fleetName;

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetName"/> struct.
        /// </summary>
        /// <param name="fleetName">Name of the fleet.</param>
        public FleetName(string fleetName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fleetName);
            _fleetName = fleetName;
        }

        /// <summary>
        /// Override method.
        /// </summary>
        /// <returns>Fleet's identifier.</returns>
        public override string ToString()
        {
            return _fleetName.ToString();
        }
    }
}
