using GtMotive.Estimate.Microservice.Application.UnitTest.Helpers;
using GtMotive.Estimate.Microservice.Application.UseCases.Fleet.AddNewVehicleToFleet;
using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;
using GtMotive.Estimate.Microservice.Domain.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Moq;

namespace GtMotive.Estimate.Microservice.Application.UnitTest.UseCases.Fleet.AddNewVehicleToFleet
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddNewVehicleToFleetHandlerTest"/> class.
    /// </summary>
    [TestFixture]
    public class AddNewVehicleToFleetHandlerTest : BaseHelpers
    {
        /// <summary>
        /// Test for AddNewVehicleToFleet use case.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task HandlerShouldReturnSuccessGivenRightInformation()
        {
            // Arrange
            var logger = new FakeLogger<AddNewVehicleToFleetHandler>();
            var request = new AddNewVehicleToFleetRequest
            {
                FleetId = BaseTestConstants.FleetIdTest.ToString(),
                VehicleBrand = BaseTestConstants.BrandNameTest,
                VehicleManufacturedOn = BaseTestConstants.InvalidManufacturedOnTest,
                VehicleModel = BaseTestConstants.ModelNameTest
            };
            FleetRepositoryMock
                .Setup(x => x.GetFleetById(It.Is<Guid>(guid => guid == Guid.Parse(request.FleetId))))
                .ReturnsAsync(new FleetDto
                {
                    FleetId = BaseTestConstants.FleetIdTest,
                    FleetName = BaseTestConstants.FleetNameTest
                });

            FleetRepositoryMock
                .Setup(x => x.AddNewVehicle(
                    It.Is<Guid>(guid => guid == Guid.Parse(request.FleetId)),
                    It.IsAny<VehicleDto>()))
                .ReturnsAsync(new FleetDto
                {
                    FleetId = BaseTestConstants.FleetIdTest,
                    FleetName = BaseTestConstants.FleetNameTest,
                    Vehicles = new List<VehicleDto>().Append(
                        new VehicleDto()
                        {
                            Brand = BaseTestConstants.BrandNameTest,
                            Model = BaseTestConstants.ModelNameTest,
                            ManufacturedOn = BaseTestConstants.ManufacturedOnTest
                        })
                });
            var handler = new AddNewVehicleToFleetHandler(FleetServiceMock.Object, logger);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.Fleet, Is.Not.Null);
                Assert.That(result!.Fleet!.Vehicles, Is.Not.Null);
                Assert.That(result.Fleet.Vehicles, Is.Not.Empty);
                Assert.That(result.Fleet.Vehicles!.First().Brand, Is.EqualTo(BaseTestConstants.BrandNameTest));
                Assert.That(result.Fleet.Vehicles!.First().Model, Is.EqualTo(BaseTestConstants.ModelNameTest));
                Assert.That(result.Fleet.Vehicles!.First().ManufacturedOn, Is.EqualTo(BaseTestConstants.ManufacturedOnTest));
                Assert.That(logger.Collector.LatestRecord, Is.Not.Null);
                Assert.That(logger.Collector.Count, Is.EqualTo(1));
                Assert.That(logger.Collector.LatestRecord.Level, Is.EqualTo(LogLevel.Information));
                Assert.That(logger.Collector.LatestRecord.Message, Does.Contain("AddNewVehicleToFleetHandler - Handle"));
            }
        }

        /// <summary>
        /// Test for AddNewVehicleToFleet use case.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task HandlerShouldReturnFailGivenNonExistingFleet()
        {
            // Arrange
            var logger = new FakeLogger<AddNewVehicleToFleetHandler>();
            var request = new AddNewVehicleToFleetRequest
            {
                FleetId = BaseTestConstants.FleetIdTest.ToString(),
                VehicleBrand = BaseTestConstants.BrandNameTest,
                VehicleManufacturedOn = BaseTestConstants.ManufacturedOnTest,
                VehicleModel = BaseTestConstants.ModelNameTest
            };
            FleetRepositoryMock
                .Setup(x => x.GetFleetById(It.Is<Guid>(guid => guid != Guid.NewGuid())))
                .ReturnsAsync((FleetDto?)null);

            var handler = new AddNewVehicleToFleetHandler(FleetServiceMock.Object, logger);

            // Act

            // Assert
            using (Assert.EnterMultipleScope())
            {
                await Assert.ThatAsync(() => handler.Handle(request, CancellationToken.None), Throws.Exception.TypeOf<ArgumentNullException>());
                Assert.That(logger.Collector.LatestRecord, Is.Not.Null);
                Assert.That(logger.Collector.Count, Is.EqualTo(2));
                Assert.That(logger.Collector.LatestRecord.Level, Is.EqualTo(LogLevel.Error));
                Assert.That(logger.Collector.LatestRecord.Message, Does.Contain("AddNewVehicleToFleetHandler - Handle"));
            }
        }
    }
}
