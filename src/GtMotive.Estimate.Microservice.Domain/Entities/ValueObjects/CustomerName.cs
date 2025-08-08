namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value object for Customer's name.
    /// </summary>
    public readonly struct CustomerName
    {
        private readonly string _customerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerName"/> struct.
        /// </summary>
        /// <param name="customerName">Provided name.</param>
        public CustomerName(string customerName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(customerName);

            _customerName = customerName;
        }

        /// <summary>
        /// Override method.
        /// </summary>
        /// <returns>Customer's name.</returns>
        public override string ToString()
        {
            return _customerName;
        }
    }
}
