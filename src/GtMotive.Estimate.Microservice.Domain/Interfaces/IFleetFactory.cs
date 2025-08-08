namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// IFleetFactory definition.
    /// </summary>
    public interface IFleetFactory
    {
        /// <summary>
        /// Add a vehicle to the vehicle's collection of the fleet.
        /// </summary>
        /// <param name="source">Provided instance of <see cref="IVehicle"/>.</param>
        /// <returns>Instance of <see cref="IFleet"/>.</returns>
        IFleet AddVehiclesToFleet(IVehicle source);

        /// <summary>
        /// Add a collection of vehicles to the fleet.
        /// </summary>
        /// <param name="sourceVehicles">Instance of collection of <see cref="IVehicle"/>.</param>
        /// <returns>Instance of <see cref="IFleet"/>.</returns>
        IFleet AddVehiclesToFleet(IEnumerable<IVehicle> sourceVehicles);

        /// <summary>
        /// Get a collection of <see cref="IVehicle"/> that are not rented at this moment.
        /// </summary>
        /// <param name="source">Provided instance of <see cref="IFleet"/>.</param>
        /// <returns>Collection of <see cref="IVehicle"/>.</returns>
        IReadOnlyCollection<IVehicle> GetAvailableVehiclesInFleet(IFleet source);
    }
}
