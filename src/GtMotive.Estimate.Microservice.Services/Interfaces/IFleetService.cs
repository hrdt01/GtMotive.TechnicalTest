using GtMotive.Estimate.Microservice.Domain.DTO;

namespace GtMotive.Estimate.Microservice.Services.Interfaces
{
    /// <summary>
    /// IFleetService definition.
    /// </summary>
    public interface IFleetService
    {
        /// <summary>
        /// Add new fleet to the company.
        /// </summary>
        /// <param name="newFleetName">Fleet's name.</param>
        /// <returns>Instance of <see cref="FleetDto"/>.</returns>
        Task<FleetDto?> AddNewFleet(string newFleetName);

        /// <summary>
        /// Add new vehicle to the collection of fleet's vehicles.
        /// </summary>
        /// <param name="sourceFleet">FleetDto instance.</param>
        /// <param name="sourceVehicle">Instance of <see cref="VehicleDto"/>.</param>
        /// <returns>Instance of <see cref="FleetDto"/>.</returns>
        Task<FleetDto?> AddNewVehicle(FleetDto sourceFleet, VehicleDto sourceVehicle);

        /// <summary>
        /// Get all available <see cref="VehicleDto"/> in the fleet.
        /// </summary>
        /// <param name="sourceFleet">Instance of <see cref="FleetDto"/>.</param>
        /// <returns>Collection of <see cref="VehicleDto"/>.</returns>
        Task<IReadOnlyCollection<VehicleDto>> GetAvailableFleetVehicles(FleetDto sourceFleet);
    }
}
