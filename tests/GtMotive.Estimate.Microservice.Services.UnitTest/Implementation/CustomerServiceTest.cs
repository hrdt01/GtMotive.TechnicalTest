using System.Collections.ObjectModel;
using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;
using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Services.Implementation;
using GtMotive.Estimate.Microservice.Services.UnitTest.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
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
            var logger = new FakeLogger<CustomerService>();
            var serviceInstance = new CustomerService(CustomerRepositoryMock.Object, FleetRepositoryMock.Object, logger);

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
            var logger = new FakeLogger<CustomerService>();
            var serviceInstance = new CustomerService(CustomerRepositoryMock.Object, FleetRepositoryMock.Object, logger);

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

        /// <summary>
        /// Test to add a new customer successfully.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task AddNewCustomerTest()
        {
            // Arrange
            var logger = new FakeLogger<CustomerService>();
            var serviceInstance = new CustomerService(CustomerRepositoryMock.Object, FleetRepositoryMock.Object, logger);

            var customerDto = new CustomerDto()
            {
                CustomerName = BaseTestConstants.CustomerNameTest
            };

            CustomerRepositoryMock
                .Setup(repo =>
                    repo.AddNewCustomer(It.Is<CustomerDto>(it => it.CustomerName == BaseTestConstants.CustomerNameTest)))
                .ReturnsAsync(customerDto);

            // Act
            var result = await serviceInstance.AddNewCustomer(BaseTestConstants.CustomerNameTest);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.CustomerName, Is.EqualTo(BaseTestConstants.CustomerNameTest));
            }
        }

        /// <summary>
        /// Test to return a rented vehicle successfully.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task ReturnRentedVehicleTestOk()
        {
            // Arrange
            var logger = new FakeLogger<CustomerService>();
            var serviceInstance = new CustomerService(CustomerRepositoryMock.Object, FleetRepositoryMock.Object, logger);

            var rentedVehicleDto = new RentedVehicleDto()
            {
                FleetId = BaseTestConstants.FleetIdTest,
                VehicleId = BaseTestConstants.VehicleIdTest,
                CustomerId = BaseTestConstants.CustomerIdTest,
                StartRent = BaseTestConstants.RentStartedOn,
                EndRent = BaseTestConstants.RentFinishedOn,
                RentedVehicleId = BaseTestConstants.RentedVehicleIdTest
            };

            var returnedRentedVehicleDto = new RentedVehicleDto()
            {
                FleetId = BaseTestConstants.FleetIdTest,
                RentedVehicleId = BaseTestConstants.RentedVehicleIdTest,
                VehicleId = BaseTestConstants.VehicleIdTest,
                CustomerId = BaseTestConstants.CustomerIdTest,
                StartRent = BaseTestConstants.RentStartedOn,
                EndRent = DateTime.UtcNow
            };

            CustomerRepositoryMock
                .Setup(repo =>
                    repo.GetRentedVehicleByIdAndCustomerId(
                        It.Is<Guid>(it => it == rentedVehicleDto.RentedVehicleId),
                        It.Is<Guid>(it => it == rentedVehicleDto.CustomerId)))
                .ReturnsAsync(rentedVehicleDto);

            CustomerRepositoryMock
                .Setup(repo =>
                    repo.ReturnRentedVehicle(It.Is<Guid>(it => it == rentedVehicleDto.RentedVehicleId)))
                .ReturnsAsync(returnedRentedVehicleDto);

            // Act
            var result = await serviceInstance.ReturnRentedVehicle(rentedVehicleDto);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.VehicleId, Is.EqualTo(rentedVehicleDto.VehicleId));
                Assert.That(result.RentedVehicleId, Is.EqualTo(rentedVehicleDto.RentedVehicleId));
                Assert.That(result.EndRent, Is.LessThan(DateTime.UtcNow));
                Assert.That(logger.Collector.Count, Is.Zero);
            }
        }

        /// <summary>
        /// Test to return a rented vehicle successfully.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task ReturnRentedVehicleTestKoBecauseRentedVehicleDoesNotBelongToCustomer()
        {
            // Arrange
            var logger = new FakeLogger<CustomerService>();
            var serviceInstance = new CustomerService(CustomerRepositoryMock.Object, FleetRepositoryMock.Object, logger);

            var rentedVehicleDto = new RentedVehicleDto()
            {
                FleetId = BaseTestConstants.FleetIdTest,
                VehicleId = BaseTestConstants.VehicleIdTest,
                CustomerId = BaseTestConstants.CustomerIdTest,
                StartRent = BaseTestConstants.RentStartedOn,
                EndRent = BaseTestConstants.RentFinishedOn,
                RentedVehicleId = BaseTestConstants.RentedVehicleIdTest
            };

            var returnedRentedVehicleDto = new RentedVehicleDto()
            {
                FleetId = BaseTestConstants.FleetIdTest,
                RentedVehicleId = BaseTestConstants.RentedVehicleIdTest,
                VehicleId = BaseTestConstants.VehicleIdTest,
                CustomerId = Guid.NewGuid(),
                StartRent = BaseTestConstants.RentStartedOn,
                EndRent = DateTime.UtcNow
            };

            CustomerRepositoryMock
                .Setup(repo =>
                    repo.GetRentedVehicleByIdAndCustomerId(
                        It.Is<Guid>(it => it == rentedVehicleDto.RentedVehicleId),
                        It.Is<Guid>(it => it == rentedVehicleDto.CustomerId)))
                .ReturnsAsync(rentedVehicleDto);

            // Act

            // Assert
            using (Assert.EnterMultipleScope())
            {
                await Assert.ThatAsync(() => serviceInstance.ReturnRentedVehicle(returnedRentedVehicleDto), Throws.Exception.TypeOf<ArgumentNullException>());
                Assert.That(logger.Collector.LatestRecord, Is.Not.Null);
                Assert.That(logger.Collector.Count, Is.EqualTo(1));
                Assert.That(logger.Collector.LatestRecord.Level, Is.EqualTo(LogLevel.Warning));
                Assert.That(logger.Collector.LatestRecord.Message, Does.Contain("CustomerService - ReturnRentedVehicle"));
            }
        }

        /// <summary>
        /// Test to return a rented vehicle successfully.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task ReturnRentedVehicleTestKoBecauseRentedVehicleIsNotTheCurrentRenting()
        {
            // Arrange
            var logger = new FakeLogger<CustomerService>();
            var serviceInstance = new CustomerService(CustomerRepositoryMock.Object, FleetRepositoryMock.Object, logger);

            var rentedVehicleDto = new RentedVehicleDto()
            {
                FleetId = BaseTestConstants.FleetIdTest,
                VehicleId = BaseTestConstants.VehicleIdTest,
                CustomerId = BaseTestConstants.CustomerIdTest,
                StartRent = BaseTestConstants.RentStartedOn,
                EndRent = BaseTestConstants.RentFinishedOn,
                RentedVehicleId = BaseTestConstants.RentedVehicleIdTest
            };

            var returnedRentedVehicleDto = new RentedVehicleDto()
            {
                FleetId = BaseTestConstants.FleetIdTest,
                RentedVehicleId = Guid.NewGuid(),
                VehicleId = BaseTestConstants.VehicleIdTest,
                CustomerId = BaseTestConstants.CustomerIdTest,
                StartRent = BaseTestConstants.RentStartedOn,
                EndRent = BaseTestConstants.RentFinishedOn
            };

            CustomerRepositoryMock
                .Setup(repo =>
                    repo.GetRentedVehicleByIdAndCustomerId(
                        It.Is<Guid>(it => it == rentedVehicleDto.RentedVehicleId),
                        It.Is<Guid>(it => it == rentedVehicleDto.CustomerId)))
                .ReturnsAsync(rentedVehicleDto);

            // Act

            // Assert
            using (Assert.EnterMultipleScope())
            {
                await Assert.ThatAsync(() => serviceInstance.ReturnRentedVehicle(returnedRentedVehicleDto), Throws.Exception.TypeOf<ArgumentNullException>());
                Assert.That(logger.Collector.LatestRecord, Is.Not.Null);
                Assert.That(logger.Collector.Count, Is.EqualTo(1));
                Assert.That(logger.Collector.LatestRecord.Level, Is.EqualTo(LogLevel.Warning));
                Assert.That(logger.Collector.LatestRecord.Message, Does.Contain("CustomerService - ReturnRentedVehicle"));
            }
        }
    }
}
