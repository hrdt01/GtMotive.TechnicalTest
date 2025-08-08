using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.Implementation
{
    /// <summary>
    /// EntityFactory implementation.
    /// </summary>
    public sealed class EntityFactory : ICustomerEntityFactory, IVehicleEntityFactory, IRentedVehicleEntityFactory, IFleetEntityFactory
    {
        /// <inheritdoc />
        public ICustomer NewCustomer(CustomerName customerName)
        {
            return new CustomerEntity(customerName);
        }

        /// <inheritdoc />
        public IVehicle NewVehicle(Brand brandName, Model modelName, ManufacturedOn manufacturedOn)
        {
            return new VehicleEntity(brandName, modelName, manufacturedOn);
        }

        /// <inheritdoc />
        public IVehicle NewVehicle(VehicleId id, Brand brandName, Model modelName, ManufacturedOn manufacturedOn)
        {
            return new VehicleEntity(id, brandName, modelName, manufacturedOn);
        }

        /// <inheritdoc />
        public IRentedVehicle NewRentedVehicle(VehicleId vehicleId, DateTime rentStartedOn, DateTime rentToFinishOn, FleetId fleetId, CustomerId customerId)
        {
            return new RentedVehicleEntity(vehicleId, rentStartedOn, rentToFinishOn, fleetId, customerId);
        }

        /// <inheritdoc />
        public IRentedVehicle NewRentedVehicle(RentedVehicleId rentedVehicleId, VehicleId vehicleId, DateTime rentStartedOn, DateTime rentToFinishOn, FleetId fleetId, CustomerId customerId)
        {
            return new RentedVehicleEntity(rentedVehicleId, vehicleId, rentStartedOn, rentToFinishOn, fleetId, customerId);
        }

        /// <inheritdoc />
        public IFleet NewFleet(FleetName fleetName)
        {
            return new FleetEntity(fleetName);
        }

        /// <inheritdoc />
        public IFleet NewFleet(FleetName fleetName, FleetVehicles fleetVehicles)
        {
            return new FleetEntity(fleetName, fleetVehicles);
        }

        /// <inheritdoc />
        public IFleet NewFleet(FleetId fleetId, FleetName fleetName, FleetVehicles fleetVehicles)
        {
            return new FleetEntity(fleetId, fleetName, fleetVehicles);
        }
    }
}
