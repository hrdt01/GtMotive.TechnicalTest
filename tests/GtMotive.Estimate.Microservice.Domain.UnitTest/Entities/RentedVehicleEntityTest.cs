using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;
using GtMotive.Estimate.Microservice.Domain.UnitTest.TestHelpers;

namespace GtMotive.Estimate.Microservice.Domain.Entities.Tests
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RentedVehicleEntityTest"/> class.
    /// </summary>
    [TestFixture]
    public class RentedVehicleEntityTest
    {
        /// <summary>
        /// Test for entity creation.
        /// </summary>
        [Test]
        public void GivenCorrectInformationThenRentedVehicleEntityCreationOk()
        {
            // Arrange
            var brandName = new Brand(BaseTestConstants.BrandNameTest);
            var modelName = new Model(BaseTestConstants.ModelNameTest);
            var manufacturedDateOn = new ManufacturedOn(BaseTestConstants.ManufacturedOnTest);
            var vehicleEntity = new VehicleEntity(brandName, modelName, manufacturedDateOn);

            var fleetName = new FleetName(BaseTestConstants.FleetNameTest);
            var fleetEntity = new FleetEntity(fleetName);

            var customerName = new CustomerName(BaseTestConstants.CustomerNameTest);
            var customerEntity = new CustomerEntity(customerName);

            var rentStartedOn = DateTime.UtcNow.AddDays(1);
            var rentToFinishOn = DateTime.UtcNow.AddDays(5);

            // Act
            var rentedVechicleEntity = new RentedVehicleEntity(vehicleEntity.Id, rentStartedOn, rentToFinishOn, fleetEntity.Id, customerEntity.Id);

            // Assert
            Assert.That(rentedVechicleEntity, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(rentedVechicleEntity.CustomerId, Is.EqualTo(customerEntity.Id));
                Assert.That(rentedVechicleEntity.FleetId, Is.EqualTo(fleetEntity.Id));
                Assert.That(rentedVechicleEntity.VehicleId, Is.EqualTo(vehicleEntity.Id));
            });
        }
    }
}
