using GtMotive.Estimate.Microservice.Domain.DTO;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Fleet.CreateNewFleet
{
    /// <summary>
    /// CreateNewFleetResponse definition.
    /// </summary>
    public class CreateNewFleetResponse
    {
        /// <summary>
        /// Gets or sets the fleet.
        /// </summary>
        public FleetDto? Fleet { get; set; }
    }
}
