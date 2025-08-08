using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Entity RentedVehicle.
    /// </summary>
    public abstract class RentedVehicle : IRentedVehicle
    {
        /// <summary>
        /// Gets or sets RentedVehicle identifier.
        /// </summary>
        public RentedVehicleId Id { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="Fleet"/> identifier.
        /// </summary>
        public FleetId FleetId { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="Vehicle"/> identifier.
        /// </summary>
        public VehicleId VehicleId { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="Customer"/> identifier.
        /// </summary>
        public CustomerId CustomerId { get; protected set; }

        /// <summary>
        /// Gets or sets the start date of the renting.
        /// </summary>
        public RentStartedOn RentStartedOn { get; protected set; }

        /// <summary>
        /// Gets or sets the finish date of the renting.
        /// </summary>
        public RentFinishedOn RentFinishedOn { get; protected set; }
    }
}
