using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;
using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Database;
using GtMotive.Estimate.Microservice.Infrastructure.Implementation;
using Microsoft.Data.Sqlite;

namespace GtMotive.Estimate.Microservice.Infrastructure.UnitTest.Implementation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerRepositoryTest"/> class.
    /// </summary>
    [TestFixture]
    public class CustomerRepositoryTest
    {
        private readonly SqliteConnection _testDbConnection;
        private readonly FleetContext? _testDbContext;
        private readonly IRentedVehicleEntityFactory testRentedVehicleEntityFactory;
        private readonly ICustomerEntityFactory testCustomerEntityEntityFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepositoryTest"/> class.
        /// </summary>
        public CustomerRepositoryTest()
        {
            _testDbConnection = TestDbContext.CreateSqliteTestConnection();
            _testDbContext = TestDbContext.CreateContext<FleetContext>(_testDbConnection);
            testRentedVehicleEntityFactory = new EntityFactory();
            testCustomerEntityEntityFactory = new EntityFactory();
        }

        /// <summary>
        /// Method to free resources.
        /// </summary>
        [OneTimeTearDown]
        public void TearDown()
        {
            _testDbConnection.Dispose();
            _testDbContext?.Dispose();
        }

        /// <summary>
        /// Cleanup teardown.
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            TestDbContext.DatabaseCleanup();
        }

        /// <summary>
        /// Test to create new customer.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task AddNewCustomerTest()
        {
            // Arrange
            var repositoryInstance = new CustomerRepository(
                _testDbContext!,
                testRentedVehicleEntityFactory,
                testCustomerEntityEntityFactory);

            var newCustomerDto = new CustomerDto
            {
                CustomerName = BaseTestConstants.CustomerNameTest
            };

            // Act
            var result = await repositoryInstance.AddNewCustomer(newCustomerDto);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result!.CustomerId.ToString(), Is.Not.Null);
                Assert.That(result.CustomerName, Is.EqualTo(newCustomerDto.CustomerName));
                Assert.That(result.RentedVehicles, Is.Null);
            }
        }

        /// <summary>
        /// Test to get a customer by its identifier.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task GetCustomerByIdTest()
        {
            // Arrange
            var repositoryInstance = new CustomerRepository(
                _testDbContext!,
                testRentedVehicleEntityFactory,
                testCustomerEntityEntityFactory);

            var newCustomerDto = new CustomerDto
            {
                CustomerName = BaseTestConstants.CustomerNameTest
            };
            var persistedCustomer = await repositoryInstance.AddNewCustomer(newCustomerDto);

            // Act
            var result = await repositoryInstance.GetCustomerById(persistedCustomer!.CustomerId);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result!.CustomerId, Is.EqualTo(persistedCustomer.CustomerId));
                Assert.That(result.CustomerName, Is.EqualTo(persistedCustomer.CustomerName));
                Assert.That(result.RentedVehicles, Is.EqualTo(persistedCustomer.RentedVehicles));
            }
        }

        /// <summary>
        /// Test to rent a vehicle.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task RentVehicleTest()
        {
            // Arrange
            TestDbContext.SeedDataToRentVehicle();
            var repositoryInstance = new CustomerRepository(
                _testDbContext!,
                testRentedVehicleEntityFactory,
                testCustomerEntityEntityFactory);

            var newCustomerDto = new CustomerDto
            {
                CustomerName = BaseTestConstants.CustomerNameTest
            };
            var persistedCustomer = await repositoryInstance.AddNewCustomer(newCustomerDto);

            var rentedVehicleDto = new RentedVehicleDto
            {
                FleetId = BaseTestConstants.FleetIdTest,
                VehicleId = BaseTestConstants.VehicleIdTest,
                CustomerId = persistedCustomer!.CustomerId,
                StartRent = DateTime.UtcNow,
                EndRent = DateTime.UtcNow.AddHours(1)
            };

            // Act
            var result = await repositoryInstance.RentVehicle(rentedVehicleDto);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.VehicleId, Is.EqualTo(BaseTestConstants.VehicleIdTest));
                Assert.That(result.FleetId, Is.EqualTo(BaseTestConstants.FleetIdTest));
                Assert.That(result.CustomerId, Is.EqualTo(persistedCustomer.CustomerId));
            }
        }

        /// <summary>
        /// Test to return a rented vehicle.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task ReturnRentedVehicleTest()
        {
            // Arrange
            TestDbContext.SeedDataToReturnRentedVehicle();
            var repositoryInstance = new CustomerRepository(
                _testDbContext!,
                testRentedVehicleEntityFactory,
                testCustomerEntityEntityFactory);

            var rentedVehicle = await repositoryInstance.GetRentedVehicleByIdAndCustomerId(
                BaseTestConstants.RentedVehicleIdTest,
                BaseTestConstants.CustomerIdTest);

            // Act
            var result = await repositoryInstance.ReturnRentedVehicle(rentedVehicle!.RentedVehicleId);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(rentedVehicle, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.EndRent, Is.LessThan(DateTime.UtcNow));
            }
        }

        /// <summary>
        /// Test to get a rented vehicle.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task GetRentedVehicleByIdAndCustomerIdTest()
        {
            // Arrange
            TestDbContext.SeedDataToReturnRentedVehicle();
            var repositoryInstance = new CustomerRepository(
                _testDbContext!,
                testRentedVehicleEntityFactory,
                testCustomerEntityEntityFactory);

            // Act
            var result = await repositoryInstance.GetRentedVehicleByIdAndCustomerId(
                BaseTestConstants.RentedVehicleIdTest,
                BaseTestConstants.CustomerIdTest);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.EndRent, Is.EqualTo(BaseTestConstants.RentFinishedOn));
            }
        }

        /// <summary>
        /// Test to get a rented vehicle.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task GetRentedVehicleByIdTest()
        {
            // Arrange
            TestDbContext.SeedDataToReturnRentedVehicle();
            var repositoryInstance = new CustomerRepository(
                _testDbContext!,
                testRentedVehicleEntityFactory,
                testCustomerEntityEntityFactory);

            // Act
            var result = await repositoryInstance.GetRentedVehicleById(BaseTestConstants.RentedVehicleIdTest);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.EndRent, Is.EqualTo(BaseTestConstants.RentFinishedOn));
            }
        }
    }
}
