using System.Globalization;
using GtMotive.Estimate.Microservice.Domain.Entities.Exceptions;

namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value object for ManufacturedOn's date.
    /// </summary>
    public readonly struct ManufacturedOn
    {
        private readonly DateTime _manufacturedOn;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturedOn"/> struct.
        /// </summary>
        /// <param name="manufacturedOn">Date when the vehicle was manufactured.</param>
        /// <exception cref="NotSupportedVehicleException">Exception raised.</exception>
        public ManufacturedOn(DateTime manufacturedOn)
        {
            ArgumentNullException.ThrowIfNull(manufacturedOn);

            if (manufacturedOn.AddYears(5) <= DateTime.UtcNow)
            {
                throw new NotSupportedVehicleException($"The value for {nameof(manufacturedOn)} field is not allowed.");
            }

            _manufacturedOn = manufacturedOn;
        }

        /// <summary>
        /// String representation.
        /// </summary>
        /// <param name="cultureInfo">Provided culture info.</param>
        /// <returns>String representation of <see cref="ManufacturedOn"/>.</returns>
        /// <exception cref="ArgumentNullException">Exception raised.</exception>
        public string ToString(CultureInfo cultureInfo)
        {
            return cultureInfo == null
                ? throw new ArgumentNullException(nameof(cultureInfo), $"The {nameof(cultureInfo)} parameter is required.")
                : _manufacturedOn.ToString(cultureInfo.DateTimeFormat);
        }

        /// <summary>
        /// Returns date value.
        /// </summary>
        /// <returns>DateTime value.</returns>
        public DateTime ToDateTime()
        {
            return _manufacturedOn;
        }
    }
}
