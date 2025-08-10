using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;
using GtMotive.Estimate.Microservice.Domain.DbEntities;
using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Infrastructure.Mappers;

namespace GtMotive.Estimate.Microservice.Infrastructure.UnitTest.Mappers
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityMapperTest"/> class.
    /// </summary>
    [TestFixture]
    public class EntityMapperTest
    {
        /// <summary>
        /// Test for map from <see cref="IDbEntity"/> instance to <see cref="IDtoEntity"/> instance.
        /// </summary>
        [Test]
        public void FromDbEntityToDtoTest()
        {
            // Arrange
            var dbEntity = new FleetVehicle
            {
                Vehicle = new Vehicle
                {
                    Brand = BaseTestConstants.BrandNameTest,
                    ManufacturedOn = BaseTestConstants.ManufacturedOnTest,
                    Model = BaseTestConstants.ModelNameTest
                }
            };

            // Act
            var result = dbEntity.FromDbEntityToDto();

            // Assert
            Assert.That(result, Is.Not.Null);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.ManufacturedOn, Is.EqualTo(dbEntity.Vehicle.ManufacturedOn));
                Assert.That(result.Brand, Is.EqualTo(dbEntity.Vehicle.Brand));
                Assert.That(result.Model, Is.EqualTo(dbEntity.Vehicle.Model));
                Assert.That(result.VehicleId, Is.EqualTo(dbEntity.Vehicle.VehicleId));
            }
        }

        /// <summary>
        /// Test for map from <see cref="IDtoEntity"/> instance to Domain instance.
        /// </summary>
        [Test]
        public void FromDtoToDomainTest()
        {
            // Arrange
            var dtoEntity = new VehicleDto
            {
                Brand = BaseTestConstants.BrandNameTest,
                ManufacturedOn = BaseTestConstants.ManufacturedOnTest,
                Model = BaseTestConstants.ModelNameTest,
                VehicleId = Guid.NewGuid()
            };

            // Act
            var result = dtoEntity.FromDtoToDomain();

            // Assert
            Assert.That(result, Is.Not.Null);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.ManufacturedOn.ToDateTime(), Is.EqualTo(dtoEntity.ManufacturedOn));
                Assert.That(result.Brand.ToString(), Is.EqualTo(dtoEntity.Brand));
                Assert.That(result.Model.ToString(), Is.EqualTo(dtoEntity.Model));
                Assert.That(result.Id.ToString(), Is.EqualTo(dtoEntity.VehicleId.ToString()));
            }
        }

        /// <summary>
        /// Test for map from Domain instance to <see cref="IDtoEntity"/> instance.
        /// </summary>
        [Test]
        public void FromDomainToDbEntityTest()
        {
            // Arrange
            var dtoEntity = new VehicleDto
            {
                Brand = BaseTestConstants.BrandNameTest,
                ManufacturedOn = BaseTestConstants.ManufacturedOnTest,
                Model = BaseTestConstants.ModelNameTest,
                VehicleId = Guid.NewGuid()
            };
            var domainEntity = dtoEntity.FromDtoToDomain();

            // Act
            var result = domainEntity.FromDomainToDbEntity();

            // Assert
            Assert.That(result, Is.Not.Null);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.ManufacturedOn, Is.EqualTo(domainEntity.ManufacturedOn.ToDateTime()));
                Assert.That(result.Brand, Is.EqualTo(domainEntity.Brand.ToString()));
                Assert.That(result.Model, Is.EqualTo(domainEntity.Model.ToString()));
                Assert.That(result.VehicleId, Is.EqualTo(dtoEntity.VehicleId));
            }
        }
    }
}
