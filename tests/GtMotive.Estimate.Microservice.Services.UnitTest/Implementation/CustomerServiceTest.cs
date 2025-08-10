using System.Collections.ObjectModel;
using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;
using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Services.Implementation;
using GtMotive.Estimate.Microservice.Services.UnitTest.Helpers;
using Microsoft.Extensions.Logging;
using Moq;

namespace GtMotive.Estimate.Microservice.Services.UnitTest.Implementation
{
    /// <inheritdoc />
    [TestFixture]
    public class CustomerServiceTest : BaseHelpers
    {
        /// <summary>
        /// Test to rent a vehicle successfully.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task RentVehicleTestOk()
        {
            // Arrange
            var logger = new Mock<ILogger<CustomerService>>();
            var serviceInstance = new CustomerService(CustomerRepositoryMock.Object, FleetRepositoryMock.Object, logger.Object);

            var vehicleDto = new VehicleDto()
            {
                VehicleId = BaseTestConstants.VehicleIdTest,
                Brand = BaseTestConstants.BrandNameTest,
                Model = BaseTestConstants.ModelNameTest,
                ManufacturedOn = BaseTestConstants.ManufacturedOnTest
            };
            var vehiclesCollection = new List<VehicleDto>() { vehicleDto };
            var availableVehiclesDto = new ReadOnlyCollection<VehicleDto>(vehiclesCollection);

            var vehicleToRentDto = new RentedVehicleDto()
            {
                FleetId = BaseTestConstants.FleetIdTest,
                VehicleId = BaseTestConstants.VehicleIdTest,
                CustomerId = BaseTestConstants.CustomerIdTest,
                StartRent = BaseTestConstants.RentStartedOn,
                EndRent = BaseTestConstants.RentFinishedOn
            };

            var rentedVehicleDto = new RentedVehicleDto()
            {
                FleetId = BaseTestConstants.FleetIdTest,
                VehicleId = BaseTestConstants.VehicleIdTest,
                CustomerId = BaseTestConstants.CustomerIdTest,
                StartRent = BaseTestConstants.RentStartedOn,
                EndRent = BaseTestConstants.RentFinishedOn,
                RentedVehicleId = BaseTestConstants.RentedVehicleIdTest
            };

            FleetRepositoryMock
                .Setup(repo =>
                    repo.GetAvailableFleetVehicles(It.Is<Guid>(it => it == BaseTestConstants.FleetIdTest)))
                .ReturnsAsync(availableVehiclesDto);

            CustomerRepositoryMock
                .Setup(repo =>
                    repo.RentVehicle(It.Is<RentedVehicleDto>(it => it == vehicleToRentDto)))
                .ReturnsAsync(rentedVehicleDto);

            // Act
            var result = await serviceInstance.RentVehicle(vehicleToRentDto);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.VehicleId, Is.EqualTo(vehicleToRentDto.VehicleId));
                Assert.That(result.RentedVehicleId, Is.EqualTo(rentedVehicleDto.RentedVehicleId));
            }
        }

        /// <summary>
        /// Test to rent a vehicle successfully.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task RentVehicleTestKoBecauseVehicleToRentNotAvailable()
        {
            // Arrange
            var logger = new Mock<ILogger<CustomerService>>();
            var serviceInstance = new CustomerService(CustomerRepositoryMock.Object, FleetRepositoryMock.Object, logger.Object);

            var vehicleDto = new VehicleDto()
            {
                VehicleId = BaseTestConstants.VehicleIdTest,
                Brand = BaseTestConstants.BrandNameTest,
                Model = BaseTestConstants.ModelNameTest,
                ManufacturedOn = BaseTestConstants.ManufacturedOnTest
            };
            var vehiclesCollection = new List<VehicleDto>() { vehicleDto };
            var availableVehiclesDto = new ReadOnlyCollection<VehicleDto>(vehiclesCollection);

            var vehicleToRentDto = new RentedVehicleDto()
            {
                FleetId = BaseTestConstants.FleetIdTest,
                VehicleId = Guid.NewGuid(),
                CustomerId = BaseTestConstants.CustomerIdTest,
                StartRent = BaseTestConstants.RentStartedOn,
                EndRent = BaseTestConstants.RentFinishedOn
            };

            FleetRepositoryMock
                .Setup(repo =>
                    repo.GetAvailableFleetVehicles(It.Is<Guid>(it => it == BaseTestConstants.FleetIdTest)))
                .ReturnsAsync(availableVehiclesDto);

            // Act
            var result = await serviceInstance.RentVehicle(vehicleToRentDto);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.Null);
            }
        }
    }
}
