namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value object for RentedVehicle's identifier.
    /// </summary>
    public readonly struct RentedVehicleId
    {
        private readonly Guid _rentedVehicleId;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentedVehicleId"/> struct.
        /// </summary>
        public RentedVehicleId()
        {
            _rentedVehicleId = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentedVehicleId"/> struct.
        /// </summary>
        /// <param name="rentedVehicleId">Vehicle identifier.</param>
        public RentedVehicleId(Guid rentedVehicleId)
        {
            _rentedVehicleId = rentedVehicleId;
        }

        /// <summary>
        /// Override method.
        /// </summary>
        /// <returns>RentedVehicle identifier.</returns>
        public override string ToString()
        {
            return _rentedVehicleId.ToString();
        }
    }
}
