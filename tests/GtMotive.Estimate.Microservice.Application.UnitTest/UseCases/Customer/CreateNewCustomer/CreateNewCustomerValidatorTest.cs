using GtMotive.Estimate.Microservice.Application.UseCases.Customer.CreateNewCustomer;
using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;

namespace GtMotive.Estimate.Microservice.Application.UnitTest.UseCases.Customer.CreateNewCustomer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateNewCustomerValidatorTest"/> class.
    /// </summary>
    [TestFixture]
    public class CreateNewCustomerValidatorTest
    {
        /// <summary>
        /// Test to validate CreateNewCustomer use case.
        /// </summary>
        /// <param name="customerName">Supplied customer name.</param>
        [TestCase(BaseTestConstants.CustomerNameTest)]
        [TestCase("other")]
        public void CreateNewCustomerValidatorTestOk(string customerName)
        {
            // Arrange
            var validator = new CreateNewCustomerValidator();
            var request = new CreateNewCustomerRequest
            {
                CustomerName = customerName
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        /// <summary>
        /// Test to validate CreateNewCustomer use case.
        /// </summary>
        /// <param name="customerName">Supplied customer name.</param>
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void CreateNewCustomerValidatorTestKo(string? customerName)
        {
            // Arrange
            var validator = new CreateNewCustomerValidator();
            var request = new CreateNewCustomerRequest
            {
                CustomerName = customerName!
            };

            // Act
            var result = validator.Validate(request);

            // Assert
            Assert.That(result.IsValid, Is.False);
        }
    }
}
