using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using Moq;

namespace GtMotive.Estimate.Microservice.Services.UnitTest.Helpers
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
        }

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
