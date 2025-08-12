using GtMotive.Estimate.Microservice.Application.UnitTest.Helpers;
using GtMotive.Estimate.Microservice.Application.UseCases.Customer.CreateNewCustomer;
using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;
using GtMotive.Estimate.Microservice.Domain.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Moq;

namespace GtMotive.Estimate.Microservice.Application.UnitTest.UseCases.Customer.CreateNewCustomer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateNewCustomerHandlerTests"/> class.
    /// </summary>
    [TestFixture]
    public class CreateNewCustomerHandlerTests : BaseHelpers
    {
        /// <summary>
        /// Test for CreateNewCustomer use case.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task HandlerShouldReturnSuccessGivenCustomerName()
        {
            // Arrange
            var logger = new FakeLogger<CreateNewCustomerHandler>();
            var request = new CreateNewCustomerRequest
            {
                CustomerName = BaseTestConstants.CustomerNameTest
            };
            CustomerRepositoryMock
                .Setup(x => x.AddNewCustomer(It.IsAny<CustomerDto>()))
                .ReturnsAsync(new CustomerDto
                {
                    CustomerId = BaseTestConstants.CustomerIdTest,
                    CustomerName = BaseTestConstants.CustomerNameTest
                });
            var handler = new CreateNewCustomerHandler(CustomerServiceMock.Object, logger);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Customer, Is.Not.Null);
                Assert.That(logger.Collector.LatestRecord, Is.Not.Null);
                Assert.That(logger.Collector.Count, Is.EqualTo(1));
                Assert.That(logger.Collector.LatestRecord.Level, Is.EqualTo(LogLevel.Information));
                Assert.That(logger.Collector.LatestRecord.Message, Does.Contain("CreateNewCustomerHandler - Handle"));
            }
        }

        /// <summary>
        /// Test for CreateNewCustomer use case.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task HandlerShouldReturnFailGivenEmptyCustomerName()
        {
            // Arrange
            var logger = new FakeLogger<CreateNewCustomerHandler>();
            var request = new CreateNewCustomerRequest
            {
                CustomerName = string.Empty
            };

            var handler = new CreateNewCustomerHandler(CustomerServiceMock.Object, logger);

            // Act

            // Assert
            using (Assert.EnterMultipleScope())
            {
                await Assert.ThatAsync(() => handler.Handle(request, CancellationToken.None), Throws.Exception.TypeOf<ArgumentException>());
                Assert.That(logger.Collector.LatestRecord, Is.Not.Null);
                Assert.That(logger.Collector.Count, Is.EqualTo(2));
                Assert.That(logger.Collector.LatestRecord.Level, Is.EqualTo(LogLevel.Error));
                Assert.That(logger.Collector.LatestRecord.Message, Does.Contain("CreateNewCustomerHandler - Handle"));
            }
        }
    }
}
