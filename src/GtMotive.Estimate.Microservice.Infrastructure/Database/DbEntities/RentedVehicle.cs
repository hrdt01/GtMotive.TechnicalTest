namespace GtMotive.Estimate.Microservice.Infrastructure.Database.DbEntities
{
    /// <summary>
    /// Entity RentedVehicle.
    /// </summary>
    public class RentedVehicle
    {
        /// <summary>
        /// Gets or sets the rented vehicle id.
        /// </summary>
        public Guid RentedVehicleId { get; set; }

        /// <summary>
        /// Gets or sets the vehicle id.
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets the vehicle.
        /// </summary>
        public Vehicle Vehicle { get; }

        /// <summary>
        /// Gets or sets the start date of the renting.
        /// </summary>
        public DateTime RentStartedOn { get; set; }

        /// <summary>
        /// Gets or sets the finish date of the renting.
        /// </summary>
        public DateTime? RentFinishedOn { get; set; }
    }
}
