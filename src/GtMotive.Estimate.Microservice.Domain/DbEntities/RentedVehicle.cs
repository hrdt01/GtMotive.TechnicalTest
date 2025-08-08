namespace GtMotive.Estimate.Microservice.Domain.DbEntities
{
    /// <summary>
    /// Entity RentedVehicle.
    /// </summary>
    public class RentedVehicle : IDbEntity
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
        public Vehicle? Vehicle { get; }

        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        public Customer? Customer { get; }

        /// <summary>
        /// Gets or sets the fleet id.
        /// </summary>
        public Guid FleetId { get; set; }

        /// <summary>
        /// Gets the fleet.
        /// </summary>
        public Fleet? Fleet { get; }

        /// <summary>
        /// Gets or sets the start date of the renting.
        /// </summary>
        public DateTime RentStartedOn { get; set; }

        /// <summary>
        /// Gets or sets the finish date of the renting.
        /// </summary>
        public DateTime RentFinishedOn { get; set; }
    }
}
