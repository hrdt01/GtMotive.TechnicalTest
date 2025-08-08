using System.Collections.ObjectModel;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value object for RentedVehicles collection.
    /// </summary>
    public readonly struct RentedVehicles
    {
        private readonly IList<IRentedVehicle> _vehiclesCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentedVehicles"/> struct.
        /// </summary>
        public RentedVehicles()
        {
            _vehiclesCollection = new List<IRentedVehicle>
            {
                Capacity = 0
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentedVehicles"/> struct.
        /// </summary>
        /// <param name="rentedVehiclesCollection">Source collection.</param>
        public RentedVehicles(IList<IRentedVehicle> rentedVehiclesCollection)
        {
            _vehiclesCollection = rentedVehiclesCollection;
        }

        /// <summary>
        /// Gets the vehicles collection.
        /// </summary>
        public IReadOnlyCollection<IRentedVehicle> Vehicles => GetRentedVehicles();

        /// <summary>
        /// Add a vehicle to the collection.
        /// Only if there are no active renting, ergo all <see cref="IRentedVehicle"/> instances have value in <see cref="RentFinishedOn"/>.
        /// </summary>
        /// <param name="vehicle">Instance of <see cref="IRentedVehicle"/> to add.</param>
        /// <exception cref="ArgumentNullException">Exception raised.</exception>
        public void RentVehicle(IRentedVehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), $"The {nameof(vehicle)} field is required.");
            }

            _vehiclesCollection.Add(vehicle);
        }

        private ReadOnlyCollection<IRentedVehicle> GetRentedVehicles()
        {
            var readOnlyCollection = new ReadOnlyCollection<IRentedVehicle>(_vehiclesCollection);
            return readOnlyCollection;
        }
    }
}
