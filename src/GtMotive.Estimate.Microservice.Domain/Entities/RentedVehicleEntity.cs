using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <inheritdoc />
    public class RentedVehicleEntity : RentedVehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentedVehicleEntity"/> class.
        /// </summary>
        /// <param name="vehicleId">Instance of <see cref="VehicleId"/>.</param>
        /// <param name="rentStartedOn">Instance of <see cref="RentStartedOn"/>.</param>
        /// <param name="rentToFinishOn">Instance of <see cref="RentFinishedOn"/>.</param>
        /// <param name="fleetId">Instance of <see cref="FleetId"/>.</param>
        /// <param name="customerId">Instance of <see cref="CustomerId"/>.</param>
        public RentedVehicleEntity(VehicleId vehicleId, DateTime rentStartedOn, DateTime rentToFinishOn, FleetId fleetId, CustomerId customerId)
        {
            Id = new RentedVehicleId(Guid.NewGuid());
            VehicleId = vehicleId;
            FleetId = fleetId;
            CustomerId = customerId;
            RentStartedOn = new RentStartedOn(rentStartedOn);
            RentFinishedOn = new RentFinishedOn(rentToFinishOn);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentedVehicleEntity"/> class.
        /// </summary>
        /// <param name="rentedVehicleId">Instance of <see cref="RentedVehicleId"/>.</param>
        /// <param name="vehicleId">Instance of <see cref="VehicleId"/>.</param>
        /// <param name="rentStartedOn">Instance of <see cref="RentStartedOn"/>.</param>
        /// <param name="rentToFinishOn">Instance of <see cref="RentFinishedOn"/>.</param>
        /// <param name="fleetId">Instance of <see cref="FleetId"/>.</param>
        /// <param name="customerId">Instance of <see cref="CustomerId"/>.</param>
        public RentedVehicleEntity(RentedVehicleId rentedVehicleId, VehicleId vehicleId, DateTime rentStartedOn, DateTime rentToFinishOn, FleetId fleetId, CustomerId customerId)
        {
            Id = rentedVehicleId;
            VehicleId = vehicleId;
            FleetId = fleetId;
            CustomerId = customerId;
            RentStartedOn = new RentStartedOn(rentStartedOn);
            RentFinishedOn = new RentFinishedOn(rentToFinishOn);
        }
    }
}
