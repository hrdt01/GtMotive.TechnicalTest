using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Entity Vehicle.
    /// </summary>
    public abstract class Vehicle : IVehicle
    {
        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        public VehicleId Id { get; protected set; }

        /// <summary>
        /// Gets or sets the vehicle's brand.
        /// </summary>
        public Brand Brand { get; protected set; }

        /// <summary>
        /// Gets or sets the vehicle's model.
        /// </summary>
        public Model Model { get; protected set; }

        /// <summary>
        /// Gets or sets the date of manufacture.
        /// </summary>
        public ManufacturedOn ManufacturedOn { get; protected set; }
    }
}
