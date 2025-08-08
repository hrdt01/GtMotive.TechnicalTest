using System.Collections.ObjectModel;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value object for FleetVehicles collection.
    /// </summary>
    public readonly struct FleetVehicles
    {
        private readonly IList<IVehicle> _vehiclesCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetVehicles"/> struct.
        /// </summary>
        public FleetVehicles()
        {
            _vehiclesCollection = new List<IVehicle>
            {
                Capacity = 0
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetVehicles"/> struct.
        /// </summary>
        /// <param name="vehiclesCollection">Source collection.</param>
        public FleetVehicles(IList<IVehicle> vehiclesCollection)
        {
            _vehiclesCollection = vehiclesCollection;
        }

        /// <summary>
        /// Gets the vehicles collection.
        /// </summary>
        public IReadOnlyCollection<IVehicle> Vehicles => GetVehicles();

        private ReadOnlyCollection<IVehicle> GetVehicles()
        {
            return new ReadOnlyCollection<IVehicle>(_vehiclesCollection);
        }
    }
}
