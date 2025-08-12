using GtMotive.Estimate.Microservice.Domain.DTO;

namespace GtMotive.Estimate.Microservice.Infrastructure.Interfaces
{
    /// <summary>
    /// ICustomerRepository definition.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Perform the renting process of a vehicle.
        /// </summary>
        /// <param name="sourceRentedVehicle">RentedVehicleDto instance.</param>
        /// <returns>Instance of <see cref="RentedVehicleDto"/>.</returns>
        Task<RentedVehicleDto?> RentVehicle(RentedVehicleDto sourceRentedVehicle);

        /// <summary>
        /// Get an instance of <see cref="RentedVehicleDto"/> by its identifier.
        /// </summary>
        /// <param name="rentedVehicleId">Rented vehicle identifier.</param>
        /// <returns>Instance of <see cref="RentedVehicleDto"/>.</returns>
        Task<RentedVehicleDto?> GetRentedVehicleById(Guid rentedVehicleId);

        /// <summary>
        /// Performs the process to return a rented vehicle.
        /// </summary>
        /// <param name="rentedVehicleId">Rented vehicle identifier.</param>
        /// <returns>Instance of <see cref="RentedVehicleDto"/>.</returns>
        Task<RentedVehicleDto?> ReturnRentedVehicle(Guid rentedVehicleId);

        /// <summary>
        /// Add new customer.
        /// </summary>
        /// <param name="newCustomer"><see cref="CustomerDto"/> instance.</param>
        /// <returns>Instance of <see cref="CustomerDto"/>.</returns>
        Task<CustomerDto?> AddNewCustomer(CustomerDto newCustomer);

        /// <summary>
        /// Get an instance of <see cref="CustomerDto"/> by its identifier.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <returns>Instance of <see cref="CustomerDto"/>.</returns>
        Task<CustomerDto?> GetCustomerById(Guid customerId);

        /// <summary>
        /// Get an instance of <see cref="RentedVehicleDto"/> by its identifier belonging to a customer.
        /// </summary>
        /// <param name="rentedVehicleId">Rented vehicle identifier.</param>
        /// <param name="customerId">Customer identifier.</param>
        /// <returns>Instance of <see cref="RentedVehicleDto"/>.</returns>
        Task<RentedVehicleDto?> GetRentedVehicleByIdAndCustomerId(Guid rentedVehicleId, Guid customerId);

        /// <summary>
        /// Get a collection of rented vehicles by customer.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <returns>Collection of <see cref="RentedVehicleDto"/>.</returns>
        Task<IEnumerable<RentedVehicleDto>?> GetRentedVehiclesByCustomerId(Guid customerId);
    }
}
