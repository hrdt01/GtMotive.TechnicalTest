using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.Interfaces
{
    /// <summary>
    /// IFleetRepository definition.
    /// </summary>
    public interface IFleetRepository
    {
        /// <summary>
        /// Add new vehicle to the collection of fleet's vehicles.
        /// </summary>
        /// <param name="fleetId">Fleet identifier.</param>
        /// <param name="sourceVehicle">Instance of <see cref="IVehicle"/>.</param>
        /// <returns>Instance of <see cref="FleetDto"/>.</returns>
        Task<FleetDto?> AddNewVehicle(Guid fleetId, IVehicle sourceVehicle);

        /// <summary>
        /// Add new fleet to the company.
        /// </summary>
        /// <param name="newFleet"><see cref="IFleet"/> instance.</param>
        /// <returns>Instance of <see cref="FleetDto"/>.</returns>
        Task<FleetDto?> AddNewFleet(IFleet newFleet);

        /// <summary>
        /// Get all available <see cref="VehicleDto"/> in the fleet.
        /// </summary>
        /// <param name="fleetId">Fleet identifier.</param>
        /// <returns>Collection of <see cref="VehicleDto"/>.</returns>
        Task<IReadOnlyCollection<VehicleDto>> GetAvailableFleetVehicles(Guid fleetId);

        /// <summary>
        /// Get an instance of <see cref="FleetDto"/> by its identifier.
        /// </summary>
        /// <param name="fleetId">Fleet identifier.</param>
        /// <returns>Instance of <see cref="FleetDto"/>.</returns>
        Task<FleetDto?> GetFleetById(Guid fleetId);
    }
}
