using MediatR;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Fleet.GetAvailableVehiclesInFleet
{
    /// <summary>
    /// Request definition.
    /// </summary>
    public class GetAvailableVehiclesInFleetRequest : IRequest<GetAvailableVehiclesInFleetResponse>, IFleetRequestContract
    {
        /// <inheritdoc />
        public string FleetId { get; set; } = null!;
    }
}
