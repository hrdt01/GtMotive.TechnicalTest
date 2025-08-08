using MediatR;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Customer.ReturnRentedVehicle
{
    /// <summary>
    /// Request definition.
    /// </summary>
    public class ReturnRentedVehicleRequest : IRequest<ReturnRentedVehicleResponse>
    {
        /// <summary>
        /// Gets or sets RentedVehicleId.
        /// </summary>
        public string RentedVehicleId { get; set; } = null!;

        /// <summary>
        /// Gets or sets CustomerId.
        /// </summary>
        public string CustomerId { get; set; } = null!;
    }
}
