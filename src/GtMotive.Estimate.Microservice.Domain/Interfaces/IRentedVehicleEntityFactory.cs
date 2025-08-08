using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// IVehicleEntityFactory definition.
    /// </summary>
    public interface IRentedVehicleEntityFactory
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IRentedVehicle"/> class.
        /// </summary>
        /// <param name="vehicleId">Instance of <see cref="VehicleId"/>.</param>
        /// <param name="rentStartedOn">Instance of <see cref="RentStartedOn"/>.</param>
        /// <param name="rentToFinishOn">Instance of <see cref="RentFinishedOn"/>.</param>
        /// <param name="fleetId">Instance of <see cref="FleetId"/>.</param>
        /// <param name="customerId">Instance of <see cref="CustomerId"/>.</param>
        /// <returns>Instance of <see cref="IRentedVehicle"/>.</returns>
        IRentedVehicle NewRentedVehicle(VehicleId vehicleId, DateTime rentStartedOn, DateTime rentToFinishOn, FleetId fleetId, CustomerId customerId);

        /// <summary>
        /// Initializes a new instance of <see cref="IRentedVehicle"/> class.
        /// </summary>
        /// <param name="rentedVehicleId">Instance of <see cref="RentedVehicleId"/>.</param>
        /// <param name="vehicleId">Instance of <see cref="VehicleId"/>.</param>
        /// <param name="rentStartedOn">Instance of <see cref="RentStartedOn"/>.</param>
        /// <param name="rentToFinishOn">Instance of <see cref="RentFinishedOn"/>.</param>
        /// <param name="fleetId">Instance of <see cref="FleetId"/>.</param>
        /// <param name="customerId">Instance of <see cref="CustomerId"/>.</param>
        /// <returns>Instance of <see cref="IRentedVehicle"/>.</returns>
        IRentedVehicle NewRentedVehicle(RentedVehicleId rentedVehicleId, VehicleId vehicleId, DateTime rentStartedOn, DateTime rentToFinishOn, FleetId fleetId, CustomerId customerId);
    }
}
