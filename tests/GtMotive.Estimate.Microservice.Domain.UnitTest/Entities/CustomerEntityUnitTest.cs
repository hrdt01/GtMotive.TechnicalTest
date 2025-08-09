using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.UnitTest.Entities
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerEntityUnitTest"/> class.
    /// </summary>
    [TestFixture]
    public class CustomerEntityUnitTest
    {
        /// <summary>
        /// Test for entity creation.
        /// </summary>
        [Test]
        public void GivenCustomerNameThenCustomerEntityCreationOk()
        {
            // Arrange
            var customerName = new CustomerName(BaseTestConstants.CustomerNameTest);

            // Act
            var customerEntity = new CustomerEntity(customerName);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(customerEntity, Is.Not.Null);
                Assert.That(customerEntity.RentedVehicles.Vehicles, Is.Empty);
            });
        }

        /// <summary>
        /// Test for entity creation.
        /// </summary>
        [Test]
        public void GivenEmptyCustomerNameThenCustomerEntityCreationThrowsException()
        {
            // Arrange
            var emptyCustomerName = string.Empty;
            CustomerName customerName;

            // Act

            // Assert
            Assert.That(
                () => { customerName = new CustomerName(emptyCustomerName); },
                Throws.TypeOf<ArgumentException>()
                .With.Message.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'customerName')"));
        }
    }
}
