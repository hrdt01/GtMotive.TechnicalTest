namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value object for Customer's identifier.
    /// </summary>
    public readonly struct CustomerId
    {
        private readonly Guid _customerId;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerId"/> struct.
        /// </summary>
        public CustomerId()
        {
            _customerId = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerId"/> struct.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        public CustomerId(Guid customerId)
        {
            _customerId = customerId;
        }

        /// <summary>
        /// Override method.
        /// </summary>
        /// <returns>Customer's identifier.</returns>
        public override string ToString()
        {
            return _customerId.ToString();
        }
    }
}
