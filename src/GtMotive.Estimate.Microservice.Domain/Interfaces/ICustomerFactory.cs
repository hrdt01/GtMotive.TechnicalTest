namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// ICustomerFactory definition.
    /// </summary>
    public interface ICustomerFactory
    {
        /// <summary>
        /// Rent a vehicle from a fleet.
        /// </summary>
        /// <param name="sourceVehicle">Instance of <see cref="IVehicle"/> to rent.</param>
        /// <param name="sourceFleet">Instance of <see cref="IFleet"/> where sourceVehicle belongs to.</param>
        /// <returns>Instance of <see cref="ICustomer"/>.</returns>
        ICustomer RentVehicle(IVehicle sourceVehicle, IFleet sourceFleet);

        /// <summary>
        /// Returns a rented vehicle finalizing the renting period.
        /// </summary>
        /// <param name="sourceRentedVehicle">Instance of <see cref="IRentedVehicle"/>.</param>
        /// <returns>Instance of <see cref="ICustomer"/>.</returns>
        ICustomer ReturnRentedVehicle(IRentedVehicle sourceRentedVehicle);
    }
}
