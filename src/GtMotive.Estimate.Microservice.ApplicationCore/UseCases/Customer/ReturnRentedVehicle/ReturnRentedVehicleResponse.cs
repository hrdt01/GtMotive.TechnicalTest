using GtMotive.Estimate.Microservice.Domain.DTO;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Customer.ReturnRentedVehicle
{
    /// <summary>
    /// ReturnRentedVehicleResponse definition.
    /// </summary>
    public class ReturnRentedVehicleResponse
    {
        /// <summary>
        /// Gets or sets the rented vehicle.
        /// </summary>
        public RentedVehicleDto? RentedVehicle { get; set; }
    }
}
