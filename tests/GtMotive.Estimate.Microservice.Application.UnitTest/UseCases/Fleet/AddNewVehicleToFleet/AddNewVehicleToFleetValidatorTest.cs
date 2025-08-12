using GtMotive.Estimate.Microservice.Application.UseCases.Fleet.AddNewVehicleToFleet;
using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;

namespace GtMotive.Estimate.Microservice.Application.UnitTest.UseCases.Fleet.AddNewVehicleToFleet
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddNewVehicleToFleetValidatorTest"/> class.
    /// </summary>
    [TestFixture]
    public class AddNewVehicleToFleetValidatorTest
    {
        /// <summary>
        /// Test to validate AddNewVehicleToFleet use case.
        /// </summary>
        [Test]
        public void AddNewVehicleToFleetValidatorTestOk()
        {
            // Arrange
            var validator = new AddNewVehicleToFleetValidator();
            var request = new AddNewVehicleToFleetRequest
            {
                FleetId = BaseTestConstants.FleetIdTest.ToString(),
                VehicleBrand = BaseTestConstants.BrandNameTest,
                VehicleManufacturedOn = BaseTestConstants.ManufacturedOnTest,
                VehicleModel = BaseTestConstants.ModelNameTest
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        /// <summary>
        /// Test to validate AddNewVehicleToFleet use case.
        /// </summary>
        [Test]
        public void CreateNewFleetValidatorTestKoInvalidVehicleManufacturedOnDate()
        {
            // Arrange
            var validator = new AddNewVehicleToFleetValidator();
            var request = new AddNewVehicleToFleetRequest
            {
                FleetId = BaseTestConstants.FleetIdTest.ToString(),
                VehicleBrand = BaseTestConstants.BrandNameTest,
                VehicleManufacturedOn = BaseTestConstants.InvalidManufacturedOnTest,
                VehicleModel = BaseTestConstants.ModelNameTest
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.That(result.IsValid, Is.False);
        }

        /// <summary>
        /// Test to validate AddNewVehicleToFleet use case.
        /// </summary>
        [Test]
        public void CreateNewFleetValidatorTestKoInvalidFleetId()
        {
            // Arrange
            var validator = new AddNewVehicleToFleetValidator();
            var request = new AddNewVehicleToFleetRequest
            {
                FleetId = string.Empty,
                VehicleBrand = BaseTestConstants.BrandNameTest,
                VehicleManufacturedOn = BaseTestConstants.InvalidManufacturedOnTest,
                VehicleModel = BaseTestConstants.ModelNameTest
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.That(result.IsValid, Is.False);
        }

        /// <summary>
        /// Test to validate AddNewVehicleToFleet use case.
        /// </summary>
        [Test]
        public void CreateNewFleetValidatorTestKoInvalidModelName()
        {
            // Arrange
            var validator = new AddNewVehicleToFleetValidator();
            var request = new AddNewVehicleToFleetRequest
            {
                FleetId = BaseTestConstants.FleetIdTest.ToString(),
                VehicleBrand = BaseTestConstants.BrandNameTest,
                VehicleManufacturedOn = BaseTestConstants.InvalidManufacturedOnTest,
                VehicleModel = string.Empty
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.That(result.IsValid, Is.False);
        }
    }
}
