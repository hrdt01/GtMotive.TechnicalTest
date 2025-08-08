namespace GtMotive.Estimate.Microservice.Domain.Entities.Exceptions
{
    /// <summary>
    /// Exception to fire when a vehicle is not supported.
    /// </summary>
    public sealed class NotSupportedVehicleException : ArgumentException
    {
        private const string DefaultReason = "Not supported vehicle";

        /// <summary>
        /// Initializes a new instance of the <see cref="NotSupportedVehicleException"/> class.
        /// </summary>
        /// <exception cref="ArgumentException">Exception raised.</exception>
        public NotSupportedVehicleException()
            : base(DefaultReason)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotSupportedVehicleException"/> class.
        /// </summary>
        /// <param name="reason">Provided reason.</param>
        /// <exception cref="ArgumentException">Exception raised.</exception>
        public NotSupportedVehicleException(string reason)
            : base(reason)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotSupportedVehicleException"/> class.
        /// </summary>
        /// <param name="reason">Provided reason.</param>
        /// <param name="innerException">Provided exception.</param>
        /// <exception cref="ArgumentException">Exception raised.</exception>
        public NotSupportedVehicleException(string reason, Exception innerException)
            : base(reason, innerException)
        {
        }
    }
}
