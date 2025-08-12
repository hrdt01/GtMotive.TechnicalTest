using GtMotive.Estimate.Microservice.Application.UnitTest.Helpers;
using GtMotive.Estimate.Microservice.Application.UseCases.Fleet.CreateNewFleet;
using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;
using GtMotive.Estimate.Microservice.Domain.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Moq;

namespace GtMotive.Estimate.Microservice.Application.UnitTest.UseCases.Fleet.CreateNewFleet
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateNewFleetHandlerTest"/> class.
    /// </summary>
    [TestFixture]
    public class CreateNewFleetHandlerTest : BaseHelpers
    {
        /// <summary>
        /// Test for CreateNewFleet use case.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task HandlerShouldReturnSuccessGivenFleetName()
        {
            // Arrange
            var logger = new FakeLogger<CreateNewFleetHandler>();
            var request = new CreateNewFleetRequest
            {
                FleetName = BaseTestConstants.FleetNameTest
            };
            FleetRepositoryMock
                .Setup(x => x.AddNewFleet(It.IsAny<FleetDto>()))
                .ReturnsAsync(new FleetDto
                {
                    FleetId = BaseTestConstants.FleetIdTest,
                    FleetName = BaseTestConstants.FleetNameTest
                });
            var handler = new CreateNewFleetHandler(FleetServiceMock.Object, logger);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Fleet, Is.Not.Null);
                Assert.That(logger.Collector.LatestRecord, Is.Not.Null);
                Assert.That(logger.Collector.Count, Is.EqualTo(1));
                Assert.That(logger.Collector.LatestRecord.Level, Is.EqualTo(LogLevel.Information));
                Assert.That(logger.Collector.LatestRecord.Message, Does.Contain("CreateNewFleetHandler - Handle"));
            }
        }

        /// <summary>
        /// Test for CreateNewFleet use case.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task HandlerShouldReturnFailGivenEmptyFleetName()
        {
            // Arrange
            var logger = new FakeLogger<CreateNewFleetHandler>();
            var request = new CreateNewFleetRequest
            {
                FleetName = string.Empty
            };

            var handler = new CreateNewFleetHandler(FleetServiceMock.Object, logger);

            // Act

            // Assert
            using (Assert.EnterMultipleScope())
            {
                await Assert.ThatAsync(() => handler.Handle(request, CancellationToken.None), Throws.Exception.TypeOf<ArgumentException>());
                Assert.That(logger.Collector.LatestRecord, Is.Not.Null);
                Assert.That(logger.Collector.Count, Is.EqualTo(2));
                Assert.That(logger.Collector.LatestRecord.Level, Is.EqualTo(LogLevel.Error));
                Assert.That(logger.Collector.LatestRecord.Message, Does.Contain("CreateNewFleetHandler - Handle"));
            }
        }
    }
}
