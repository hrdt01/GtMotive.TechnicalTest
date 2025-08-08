namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value object for Brand's name.
    /// </summary>
    public readonly struct Brand
    {
        private readonly string _brand;

        /// <summary>
        /// Initializes a new instance of the <see cref="Brand"/> struct.
        /// </summary>
        /// <param name="brand">Provided brand.</param>
        public Brand(string brand)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(brand);
            _brand = brand;
        }

        /// <summary>
        /// Override method.
        /// </summary>
        /// <returns>Brand's name.</returns>
        public override string ToString()
        {
            return _brand;
        }
    }
}
