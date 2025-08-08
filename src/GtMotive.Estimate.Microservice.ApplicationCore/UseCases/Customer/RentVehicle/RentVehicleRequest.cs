using MediatR;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Customer.RentVehicle
{
    /// <summary>
    /// Request definition.
    /// </summary>
    public class RentVehicleRequest : IRequest<RentVehicleResponse>, IFleetRequestContract
    {
        /// <inheritdoc />
        public string FleetId { get; set; } = null!;

        /// <summary>
        /// Gets or sets CustomerId.
        /// </summary>
        public string CustomerId { get; set; } = null!;

        /// <summary>
        /// Gets or sets VehicleId.
        /// </summary>
        public string VehicleId { get; set; } = null!;

        /// <summary>
        /// Gets or sets StartRent.
        /// </summary>
        public DateTime StartRent { get; set; }

        /// <summary>
        /// Gets or sets EndRent.
        /// </summary>
        public DateTime EndRent { get; set; }
    }
}
