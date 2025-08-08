using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// ICustomerEntityFactory definition.
    /// </summary>
    public interface IFleetEntityFactory
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IFleet"/> class.
        /// </summary>
        /// <param name="fleetName">Fleet name.</param>
        /// <returns>Instance of <see cref="IFleet"/>.</returns>
        IFleet NewFleet(FleetName fleetName);

        /// <summary>
        /// Initializes a new instance of <see cref="IFleet"/> class.
        /// </summary>
        /// <param name="fleetName">Fleet name.</param>
        /// <param name="fleetVehicles">FleetVehicles collection.</param>
        /// <returns>Instance of <see cref="IFleet"/>.</returns>
        IFleet NewFleet(FleetName fleetName, FleetVehicles fleetVehicles);

        /// <summary>
        /// Initializes a new instance of <see cref="IFleet"/> class.
        /// </summary>
        /// <param name="fleetId">Fleet identifier.</param>
        /// <param name="fleetName">Fleet name.</param>
        /// <param name="fleetVehicles">FleetVehicles collection.</param>
        /// <returns>Instance of <see cref="IFleet"/>.</returns>
        IFleet NewFleet(FleetId fleetId, FleetName fleetName, FleetVehicles fleetVehicles);
    }
}
