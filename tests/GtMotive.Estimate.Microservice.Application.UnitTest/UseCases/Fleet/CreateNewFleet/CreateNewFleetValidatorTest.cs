using GtMotive.Estimate.Microservice.Application.UseCases.Fleet.CreateNewFleet;
using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;

namespace GtMotive.Estimate.Microservice.Application.UnitTest.UseCases.Fleet.CreateNewFleet
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateNewFleetValidatorTest"/> class.
    /// </summary>
    [TestFixture]
    public class CreateNewFleetValidatorTest
    {
        /// <summary>
        /// Test to validate CreateNewFleet use case.
        /// </summary>
        /// <param name="fleetName">Supplied fleet name.</param>
        [TestCase(BaseTestConstants.FleetNameTest)]
        [TestCase("other")]
        public void CreateNewFleetValidatorTestOk(string fleetName)
        {
            // Arrange
            var validator = new CreateNewFleetValidator();
            var request = new CreateNewFleetRequest
            {
                FleetName = fleetName
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        /// <summary>
        /// Test to validate CreateNewFleet use case.
        /// </summary>
        /// <param name="fleetName">Supplied fleet name.</param>
        [TestCase("")]
        [TestCase(" ")]
        public void CreateNewFleetValidatorTestKo(string fleetName)
        {
            // Arrange
            var validator = new CreateNewFleetValidator();
            var request = new CreateNewFleetRequest
            {
                FleetName = fleetName!
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.That(result.IsValid, Is.False);
        }
    }
}
