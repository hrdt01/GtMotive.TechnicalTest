namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value object for Vehicle's identifier.
    /// </summary>
    public readonly struct VehicleId
    {
        private readonly Guid _vehicleId;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleId"/> struct.
        /// </summary>
        public VehicleId()
        {
            _vehicleId = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleId"/> struct.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        public VehicleId(Guid vehicleId)
        {
            _vehicleId = vehicleId;
        }

        /// <summary>
        /// Override method.
        /// </summary>
        /// <returns>Vehicle identifier.</returns>
        public override string ToString()
        {
            return _vehicleId.ToString();
        }
    }
}
