namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value object for Model's name.
    /// </summary>
    public readonly struct Model
    {
        private readonly string _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> struct.
        /// </summary>
        /// <param name="model">Provided model.</param>
        public Model(string model)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(model);

            _model = model;
        }

        /// <summary>
        /// Override method.
        /// </summary>
        /// <returns>Model's name.</returns>
        public override string ToString()
        {
            return _model;
        }
    }
}
