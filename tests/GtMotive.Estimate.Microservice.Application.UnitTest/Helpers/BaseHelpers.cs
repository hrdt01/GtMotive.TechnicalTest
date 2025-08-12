using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using GtMotive.Estimate.Microservice.Services.Implementation;
using Microsoft.Extensions.Logging.Testing;
using Moq;

namespace GtMotive.Estimate.Microservice.Application.UnitTest.Helpers
{
    /// <summary>
    /// BaseHelpers class.
    /// </summary>
    public class BaseHelpers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseHelpers"/> class.
        /// </summary>
        public BaseHelpers()
        {
            CustomerRepositoryMock = new Mock<ICustomerRepository>();
            FleetRepositoryMock = new Mock<IFleetRepository>();

            CustomerServiceMock = new Mock<CustomerService>(
                CustomerRepositoryMock.Object,
                FleetRepositoryMock.Object,
                new FakeLogger<CustomerService>());

            FleetServiceMock = new Mock<FleetService>(
                FleetRepositoryMock.Object,
                new FakeLogger<FleetService>());
        }

        /// <summary>
        /// Gets the mock instance.
        /// </summary>
        public Mock<FleetService> FleetServiceMock { get; }

        /// <summary>
        /// Gets the mock instance.
        /// </summary>
        public Mock<CustomerService> CustomerServiceMock { get; }

        /// <summary>
        /// Gets the mock instance.
        /// </summary>
        public Mock<IFleetRepository> FleetRepositoryMock { get; }

        /// <summary>
        /// Gets the mock instance.
        /// </summary>
        public Mock<ICustomerRepository> CustomerRepositoryMock { get; }
    }
}
