using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Entities.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.UnitTest.Entities
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleEntityTest"/> class.
    /// </summary>
    [TestFixture]
    public class VehicleEntityTest
    {
        /// <summary>
        /// Test for entity creation.
        /// </summary>
        [Test]
        public void GivenCorrectInformationThenVehicleEntityCreationOk()
        {
            // Arrange
            var brandName = new Brand(BaseTestConstants.BrandNameTest);
            var modelName = new Model(BaseTestConstants.ModelNameTest);
            var manufacturedDateOn = new ManufacturedOn(BaseTestConstants.ManufacturedOnTest);

            // Act
            var vehicleEntity = new VehicleEntity(brandName, modelName, manufacturedDateOn);

            // Assert
            Assert.That(vehicleEntity, Is.Not.Null);
        }

        /// <summary>
        /// Test for entity creation.
        /// </summary>
        [Test]
        public void GivenEmptyBrandNameThenVehicleEntityCreationThrowsException()
        {
            // Arrange
            var emptyBrandName = string.Empty;

            // Arrange
            Brand brand;

            // Act

            // Assert
            Assert.That(
                () => { brand = new Brand(emptyBrandName); },
                Throws.TypeOf<ArgumentException>()
                .With.Message.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'brand')"));
        }

        /// <summary>
        /// Test for entity creation.
        /// </summary>
        [Test]
        public void GivenEmptyModelNameThenVehicleEntityCreationThrowsException()
        {
            // Arrange
            var emptyModelName = string.Empty;

            // Arrange
            Model model;

            // Act

            // Assert
            Assert.That(
                () => { model = new Model(emptyModelName); },
                Throws.TypeOf<ArgumentException>()
                .With.Message.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'model')"));
        }

        /// <summary>
        /// Test for entity creation.
        /// </summary>
        [Test]
        public void GivenWrongManufacturedOnDateThenVehicleEntityCreationThrowsException()
        {
            // Arrange
            var wrongManufacturedOnDate = DateTime.MinValue;

            // Arrange
            ManufacturedOn manufacturedOn;

            // Act

            // Assert
            Assert.That(
                () => { manufacturedOn = new ManufacturedOn(wrongManufacturedOnDate); },
                Throws.TypeOf<NotSupportedVehicleException>()
                .With.Message.EqualTo("The value for manufacturedOn field is not allowed."));
        }

        /// <summary>
        /// Test for entity creation.
        /// </summary>
        [Test]
        public void GivenAlmostWrongManufacturedOnDateThenVehicleEntityCreationThrowsException()
        {
            // Arrange
            var wrongManufacturedOnDate = DateTime.UtcNow.AddYears(-5);

            // Arrange
            ManufacturedOn manufacturedOn;

            // Act

            // Assert
            Assert.That(
                () => { manufacturedOn = new ManufacturedOn(wrongManufacturedOnDate); },
                Throws.InstanceOf<NotSupportedVehicleException>()
                .With.Message.EqualTo("The value for manufacturedOn field is not allowed."));
        }
    }
}
