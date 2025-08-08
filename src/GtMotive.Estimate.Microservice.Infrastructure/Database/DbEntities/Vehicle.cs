namespace GtMotive.Estimate.Microservice.Infrastructure.Database.DbEntities
{
    /// <summary>
    /// Db Entity Vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets or sets the vehicle id.
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the manufactured on.
        /// </summary>
        public DateTime ManufacturedOn { get; set; }
    }
}
