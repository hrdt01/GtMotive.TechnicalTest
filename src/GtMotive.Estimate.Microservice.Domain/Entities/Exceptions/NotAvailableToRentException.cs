namespace GtMotive.Estimate.Microservice.Domain.Entities.Exceptions
{
    /// <summary>
    /// Exception to fire when a vehicle is not able to rent.
    /// </summary>
    public sealed class NotAvailableToRentException : ArgumentException
    {
        private const string DefaultReason = "Not available to rent";

        /// <summary>
        /// Initializes a new instance of the <see cref="NotAvailableToRentException"/> class.
        /// </summary>
        /// <exception cref="ArgumentException">Exception raised.</exception>
        public NotAvailableToRentException()
            : base(DefaultReason)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotAvailableToRentException"/> class.
        /// </summary>
        /// <param name="reason">Provided reason.</param>
        /// <exception cref="ArgumentException">Exception raised.</exception>
        public NotAvailableToRentException(string reason)
            : base(reason)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotAvailableToRentException"/> class.
        /// </summary>
        /// <param name="reason">Provided reason.</param>
        /// <param name="innerException">Provided exception.</param>
        /// <exception cref="ArgumentException">Exception raised.</exception>
        public NotAvailableToRentException(string reason, Exception innerException)
            : base(reason, innerException)
        {
        }
    }
}
