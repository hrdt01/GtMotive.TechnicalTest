using GtMotive.Estimate.Microservice.Domain.DTO;

namespace GtMotive.Estimate.Microservice.Services.Interfaces
{
    /// <summary>
    /// ICustomService definition.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Perform the renting process of a vehicle.
        /// </summary>
        /// <param name="source">RentedVehicleDto instance.</param>
        /// <returns>Instance of <see cref="RentedVehicleDto"/>.</returns>
        Task<RentedVehicleDto?> RentVehicle(RentedVehicleDto source);

        /// <summary>
        /// Performs the process to return a rented vehicle.
        /// </summary>
        /// <param name="rentedVehicle">RentedVehicleDto instance.</param>
        /// <returns>Instance of <see cref="RentedVehicleDto"/>.</returns>
        Task<RentedVehicleDto?> ReturnRentedVehicle(RentedVehicleDto rentedVehicle);

        /// <summary>
        /// Add new customer.
        /// </summary>
        /// <param name="newCustomerName">Customer's name.</param>
        /// <returns>Instance of <see cref="CustomerDto"/>.</returns>
        Task<CustomerDto?> AddNewCustomer(string newCustomerName);

        /// <summary>
        /// Check if a customer trying to rent a vehicle still has active rented vehicles.
        /// </summary>
        /// <param name="source">RentedVehicleDto instance.</param>
        /// <returns>Boolean flag.</returns>
        Task<bool> CustomerHasActiveRentedVehicles(RentedVehicleDto source);
    }
}
