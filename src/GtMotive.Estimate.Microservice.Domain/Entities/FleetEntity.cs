using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <inheritdoc />
    public class FleetEntity : Fleet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FleetEntity"/> class.
        /// </summary>
        /// <param name="fleetName">Fleet name.</param>
        public FleetEntity(FleetName fleetName)
        {
            Id = new FleetId(Guid.NewGuid());
            FleetName = fleetName;
            FleetVehicles = new FleetVehicles(Enumerable.Empty<IVehicle>().ToList());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetEntity"/> class.
        /// </summary>
        /// <param name="fleetId">Fleet identifier.</param>
        /// <param name="fleetName">Fleet name.</param>
        /// <param name="fleetVehicles">FleetVehicles collection.</param>
        public FleetEntity(FleetId fleetId, FleetName fleetName, FleetVehicles fleetVehicles)
        {
            Id = fleetId;
            FleetName = fleetName;
            FleetVehicles = fleetVehicles;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetEntity"/> class.
        /// </summary>
        /// <param name="fleetName">Fleet name.</param>
        /// <param name="fleetVehicles">FleetVehicles collection.</param>
        public FleetEntity(FleetName fleetName, FleetVehicles fleetVehicles)
        {
            Id = default;
            FleetName = fleetName;
            FleetVehicles = fleetVehicles;
        }
    }
}
