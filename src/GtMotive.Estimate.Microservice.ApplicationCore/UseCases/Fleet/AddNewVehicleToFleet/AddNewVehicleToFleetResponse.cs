using GtMotive.Estimate.Microservice.Domain.DTO;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Fleet.AddNewVehicleToFleet
{
    /// <summary>
    /// AddNewVehicleToFleetResponse definition.
    /// </summary>
    public class AddNewVehicleToFleetResponse
    {
        /// <summary>
        /// Gets or sets the fleet.
        /// </summary>
        public FleetDto? Fleet { get; set; }
    }
}
