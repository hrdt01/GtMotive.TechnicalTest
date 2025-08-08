using GtMotive.Estimate.Microservice.Domain.DTO;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Fleet.GetAvailableVehiclesInFleet
{
    /// <summary>
    /// GetAvailableVehiclesInFleetResponse definition.
    /// </summary>
    public class GetAvailableVehiclesInFleetResponse
    {
        /// <summary>
        /// Gets or sets the vehicles collection.
        /// </summary>
        public IReadOnlyCollection<VehicleDto>? Vehicles { get; set; }
    }
}
