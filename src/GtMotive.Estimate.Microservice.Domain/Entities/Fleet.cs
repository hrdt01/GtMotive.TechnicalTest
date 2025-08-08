using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Entity Fleet.
    /// </summary>
    public abstract class Fleet : IFleet
    {
        /// <summary>
        /// Gets or sets Fleet identifier.
        /// </summary>
        public FleetId Id { get; protected set; }

        /// <summary>
        /// Gets or sets Fleet name.
        /// </summary>
        public FleetName FleetName { get; protected set; }

        /// <summary>
        /// Gets or sets collection of vehicles in the Fleet.
        /// </summary>
        public FleetVehicles FleetVehicles { get; protected set; }
    }
}
