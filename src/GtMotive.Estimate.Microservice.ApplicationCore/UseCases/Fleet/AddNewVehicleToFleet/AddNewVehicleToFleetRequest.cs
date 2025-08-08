using MediatR;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Fleet.AddNewVehicleToFleet
{
    /// <summary>
    /// Request definition.
    /// </summary>
    public class AddNewVehicleToFleetRequest : IRequest<AddNewVehicleToFleetResponse>, IFleetRequestContract
    {
        /// <inheritdoc />
        public string FleetId { get; set; } = null!;

        /// <summary>
        /// Gets or sets vehicle's brand.
        /// </summary>
        public string VehicleBrand { get; set; } = null!;

        /// <summary>
        /// Gets or sets vehicle's model.
        /// </summary>
        public string VehicleModel { get; set; } = null!;

        /// <summary>
        /// Gets or sets vehicle's manufacturedOn date.
        /// </summary>
        public DateTime VehicleManufacturedOn { get; set; }
    }
}
