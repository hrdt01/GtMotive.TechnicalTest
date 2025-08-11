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
    public class FleetServiceTest : BaseHelpers
    {
        /// <summary>
        /// Test to add a new fleet successfully.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task AddNewFleetTestOk()
        {
            // Arrange
            var logger = new FakeLogger<FleetService>();
            var serviceInstance = new FleetService(FleetRepositoryMock.Object, logger);

            var fleetDto = new FleetDto()
            {
                FleetName = BaseTestConstants.FleetNameTest
            };

            FleetRepositoryMock
                .Setup(repo =>
                    repo.AddNewFleet(It.Is<FleetDto>(it => it.FleetName == BaseTestConstants.FleetNameTest)))
                .ReturnsAsync(fleetDto);

            // Act
            var result = await serviceInstance.AddNewFleet(BaseTestConstants.FleetNameTest);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.FleetName, Is.EqualTo(BaseTestConstants.FleetNameTest));
            }
        }

        /// <summary>
        /// Test to add a new vehicle to fleet successfully.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task AddNewVehicleTestOk()
        {
            // Arrange
            var logger = new FakeLogger<FleetService>();
            var serviceInstance = new FleetService(FleetRepositoryMock.Object, logger);

            var fleetDto = new FleetDto()
            {
                FleetName = BaseTestConstants.FleetNameTest,
                FleetId = BaseTestConstants.FleetIdTest
            };

            var vehicleDto = new VehicleDto()
            {
                VehicleId = BaseTestConstants.VehicleIdTest,
                Brand = BaseTestConstants.BrandNameTest,
                Model = BaseTestConstants.ModelNameTest,
                ManufacturedOn = BaseTestConstants.ManufacturedOnTest
            };

            FleetRepositoryMock
                .Setup(repo =>
                    repo.GetFleetById(It.IsAny<Guid>()))
                .ReturnsAsync(fleetDto);

            var resultFleetDto = new FleetDto()
            {
                FleetId = BaseTestConstants.FleetIdTest,
                FleetName = BaseTestConstants.FleetNameTest,
                Vehicles = new List<VehicleDto>().Append(vehicleDto)
            };

            FleetRepositoryMock
                .Setup(repo =>
                    repo.AddNewVehicle(It.IsAny<Guid>(), It.Is<VehicleDto>(it => it.VehicleId == BaseTestConstants.VehicleIdTest)))
                .ReturnsAsync(resultFleetDto);

            // Act
            var result = await serviceInstance.AddNewVehicle(fleetDto, vehicleDto);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.FleetName, Is.EqualTo(BaseTestConstants.FleetNameTest));
                Assert.That(result!.Vehicles, Is.Not.Empty);
                Assert.That(result!.Vehicles.First(), Is.EqualTo(vehicleDto));
                Assert.That(logger.Collector.Count, Is.Zero);
            }
        }

        /// <summary>
        /// Test to add a new vehicle to fleet successfully.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task AddNewVehicleTestKoBecauseTargetFleetDoesNotExist()
        {
            // Arrange
            var logger = new FakeLogger<FleetService>();
            var serviceInstance = new FleetService(FleetRepositoryMock.Object, logger);

            var fleetDto = new FleetDto()
            {
                FleetName = BaseTestConstants.FleetNameTest,
                FleetId = BaseTestConstants.FleetIdTest
            };

            var vehicleDto = new VehicleDto()
            {
                VehicleId = BaseTestConstants.VehicleIdTest,
                Brand = BaseTestConstants.BrandNameTest,
                Model = BaseTestConstants.ModelNameTest,
                ManufacturedOn = BaseTestConstants.ManufacturedOnTest
            };

            FleetRepositoryMock
                .Setup(repo =>
                    repo.GetFleetById(It.Is<Guid>(it => it == BaseTestConstants.FleetIdTest)))
                .ReturnsAsync(fleetDto);

            var otherFleetDto = new FleetDto()
            {
                FleetId = BaseTestConstants.OtherFleetIdTest,
                FleetName = BaseTestConstants.FleetNameTest
            };

            // Act

            // Assert
            using (Assert.EnterMultipleScope())
            {
                await Assert.ThatAsync(() => serviceInstance.AddNewVehicle(otherFleetDto, vehicleDto), Throws.Exception.TypeOf<ArgumentNullException>());
                Assert.That(logger.Collector.LatestRecord, Is.Not.Null);
                Assert.That(logger.Collector.Count, Is.EqualTo(1));
                Assert.That(logger.Collector.LatestRecord.Level, Is.EqualTo(LogLevel.Warning));
                Assert.That(logger.Collector.LatestRecord.Message, Does.Contain("FleetService - AddNewVehicle"));
            }
        }
    }
}
