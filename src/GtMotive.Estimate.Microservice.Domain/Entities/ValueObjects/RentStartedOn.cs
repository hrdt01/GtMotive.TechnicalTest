using System.Globalization;

namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value object for RentStartedOn's date.
    /// </summary>
    public readonly struct RentStartedOn
    {
        private readonly DateTime _rentStartedOnOn;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentStartedOn"/> struct.
        /// </summary>
        /// <param name="rentStartedOn">Date to start the renting.</param>
        public RentStartedOn(DateTime rentStartedOn)
        {
            ArgumentNullException.ThrowIfNull(rentStartedOn);
            _rentStartedOnOn = rentStartedOn;
        }

        /// <summary>
        /// Overrides method.
        /// </summary>
        /// <param name="cultureInfo">Provided culture info.</param>
        /// <returns>String representation of <see cref="RentStartedOn"/>.</returns>
        /// <exception cref="ArgumentNullException">Exception raised.</exception>
        public string ToString(CultureInfo cultureInfo)
        {
            return cultureInfo == null
                ? throw new ArgumentNullException(nameof(cultureInfo), $"The {nameof(cultureInfo)} parameter is required.")
                : _rentStartedOnOn.ToString(cultureInfo.DateTimeFormat);
        }

        /// <summary>
        /// Returns date value.
        /// </summary>
        /// <returns>DateTime value.</returns>
        public DateTime ToDateTime()
        {
            return _rentStartedOnOn;
        }
    }
}
