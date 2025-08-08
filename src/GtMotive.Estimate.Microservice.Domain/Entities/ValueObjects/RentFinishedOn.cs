using System.Globalization;

namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value object for RentFinishedOn's date.
    /// </summary>
    public readonly struct RentFinishedOn
    {
        private readonly DateTime _rentFinishedOn;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentFinishedOn"/> struct.
        /// </summary>
        /// <param name="rentFinishedOn">Date to finish the renting.</param>
        public RentFinishedOn(DateTime rentFinishedOn)
        {
            ArgumentNullException.ThrowIfNull(rentFinishedOn);

            _rentFinishedOn = rentFinishedOn;
        }

        /// <summary>
        /// Overrides method.
        /// </summary>
        /// <param name="cultureInfo">Provided culture info.</param>
        /// <returns>String representation of  <see cref="RentFinishedOn"/>.</returns>
        /// <exception cref="ArgumentNullException">Exception raised.</exception>
        public string ToString(CultureInfo cultureInfo)
        {
            return cultureInfo == null
                ? throw new ArgumentNullException(nameof(cultureInfo), $"The {nameof(cultureInfo)} parameter is required.")
                : _rentFinishedOn.ToString(cultureInfo.DateTimeFormat);
        }

        /// <summary>
        /// Returns date value.
        /// </summary>
        /// <returns>DateTime value.</returns>
        public DateTime ToDateTime()
        {
            return _rentFinishedOn;
        }
    }
}
