using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.UnitTest.Entities
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FleetEntityTest"/> class.
    /// </summary>
    [TestFixture]
    public class FleetEntityTest
    {
        /// <summary>
        /// Test for entity creation.
        /// </summary>
        [Test]
        public void GivenFleetNameThenFleetEntityCreationOk()
        {
            // Arrange
            var fleetName = new FleetName(BaseTestConstants.FleetNameTest);

            // Act
            var fleetEntity = new FleetEntity(fleetName);

            // Assert
            Assert.That(fleetEntity, Is.Not.Null);
            Assert.That(fleetEntity.FleetVehicles.Vehicles, Is.Empty);
        }

        /// <summary>
        /// Test for entity creation.
        /// </summary>
        [Test]
        public void GivenEmptyFleetNameThenFleetEntityCreationThrowsException()
        {
            // Arrange
            var emptyFleetName = string.Empty;
            FleetName fleetName;

            // Act

            // Assert
            Assert.That(
                () => { fleetName = new FleetName(emptyFleetName); },
                Throws.TypeOf<ArgumentException>()
                .With.Message.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'fleetName')"));
        }
    }
}
