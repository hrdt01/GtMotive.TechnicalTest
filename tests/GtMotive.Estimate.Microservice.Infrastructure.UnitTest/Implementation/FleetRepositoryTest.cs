using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;
using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Database;
using GtMotive.Estimate.Microservice.Infrastructure.Implementation;
using Microsoft.Data.Sqlite;

namespace GtMotive.Estimate.Microservice.Infrastructure.UnitTest.Implementation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FleetRepositoryTest"/> class.
    /// </summary>
    [TestFixture]
    public class FleetRepositoryTest
    {
        private readonly SqliteConnection _testDbConnection;
        private readonly FleetContext? _testDbContext;
        private readonly IFleetEntityFactory _testFleetEntityFactory;
        private readonly IVehicleEntityFactory _testVehicleEntityFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetRepositoryTest"/> class.
        /// </summary>
        public FleetRepositoryTest()
        {
            _testDbConnection = TestDbContext.CreateSqliteTestConnection();
            _testDbContext = TestDbContext.CreateContext<FleetContext>(_testDbConnection);
            _testFleetEntityFactory = new EntityFactory();
            _testVehicleEntityFactory = new EntityFactory();
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
        /// Test to create new fleet.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task AddNewFleetTest()
        {
            // Arrange
            var repositoryInstance = new FleetRepository(
                _testDbContext!,
                _testFleetEntityFactory,
                _testVehicleEntityFactory);

            var newFleetDto = new FleetDto
            {
                FleetName = BaseTestConstants.FleetNameTest
            };

            // Act
            var result = await repositoryInstance.AddNewFleet(newFleetDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result!.FleetId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.FleetName, Is.EqualTo(newFleetDto.FleetName));
                Assert.That(result.Vehicles, Is.Empty);
            });
        }

        /// <summary>
        /// Test to create new vehicle.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task AddNewVehicleTest()
        {
            // Arrange
            var repositoryInstance = new FleetRepository(
                _testDbContext!,
                _testFleetEntityFactory,
                _testVehicleEntityFactory);

            var newFleetDto = new FleetDto
            {
                FleetName = BaseTestConstants.FleetNameTest
            };
            var persistedFleet = await repositoryInstance.AddNewFleet(newFleetDto);
            var newVehicle = new VehicleDto()
            {
                Brand = BaseTestConstants.BrandNameTest,
                Model = BaseTestConstants.ModelNameTest,
                ManufacturedOn = BaseTestConstants.ManufacturedOnTest
            };

            // Act
            var result = await repositoryInstance.AddNewVehicle(persistedFleet!.FleetId, newVehicle);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result!.FleetId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.FleetId, Is.EqualTo(persistedFleet.FleetId));
                Assert.That(result.FleetName, Is.EqualTo(persistedFleet.FleetName));
                Assert.That(result.Vehicles, Is.Not.Empty);
                Assert.That(result.Vehicles!.First().VehicleId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Vehicles!.First().Brand, Is.EqualTo(BaseTestConstants.BrandNameTest));
                Assert.That(result.Vehicles!.First().Model, Is.EqualTo(BaseTestConstants.ModelNameTest));
                Assert.That(result.Vehicles!.First().ManufacturedOn, Is.EqualTo(BaseTestConstants.ManufacturedOnTest));
            });
        }

        /// <summary>
        /// Test to get fleet by id.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task GetFleetByIdTest()
        {
            // Arrange
            var repositoryInstance = new FleetRepository(
                _testDbContext!,
                _testFleetEntityFactory,
                _testVehicleEntityFactory);

            var newFleetDto = new FleetDto
            {
                FleetName = BaseTestConstants.FleetNameTest
            };

            var persistedFleet = await repositoryInstance.AddNewFleet(newFleetDto);

            // Act
            var result = await repositoryInstance.GetFleetById(persistedFleet!.FleetId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result!.FleetId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.FleetId, Is.EqualTo(persistedFleet.FleetId));
                Assert.That(result.FleetName, Is.EqualTo(persistedFleet.FleetName));
                Assert.That(result.Vehicles, Is.Empty);
            });
        }

        /// <summary>
        /// Test to get available vehicles in a fleet.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task GivenFleetWithNoVehiclesGetAvailableFleetVehiclesReturnsEmptyCollection()
        {
            // Arrange
            var repositoryInstance = new FleetRepository(
                _testDbContext!,
                _testFleetEntityFactory,
                _testVehicleEntityFactory);

            var newFleetDto = new FleetDto
            {
                FleetName = BaseTestConstants.FleetNameTest
            };

            var persistedFleet = await repositoryInstance.AddNewFleet(newFleetDto);

            // Act
            var result =
                await repositoryInstance.GetAvailableFleetVehicles(persistedFleet!.FleetId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Empty);
            });
        }

        /// <summary>
        /// Test to get available vehicles in a fleet.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task GivenFleetWithVehiclesGetAvailableFleetVehiclesReturnsNotEmptyCollection()
        {
            // Arrange
            var repositoryInstance = new FleetRepository(
                _testDbContext!,
                _testFleetEntityFactory,
                _testVehicleEntityFactory);

            var newFleetDto = new FleetDto
            {
                FleetName = BaseTestConstants.FleetNameTest
            };
            var persistedFleet = await repositoryInstance.AddNewFleet(newFleetDto);
            var newVehicle = new VehicleDto()
            {
                Brand = BaseTestConstants.BrandNameTest,
                Model = BaseTestConstants.ModelNameTest,
                ManufacturedOn = BaseTestConstants.ManufacturedOnTest
            };
            _ = await repositoryInstance.AddNewVehicle(persistedFleet!.FleetId, newVehicle);

            // Act
            var result =
                await repositoryInstance.GetAvailableFleetVehicles(persistedFleet.FleetId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Not.Empty);
                Assert.That(result.First().VehicleId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.First().Brand, Is.EqualTo(BaseTestConstants.BrandNameTest));
                Assert.That(result.First().Model, Is.EqualTo(BaseTestConstants.ModelNameTest));
                Assert.That(result.First().ManufacturedOn, Is.EqualTo(BaseTestConstants.ManufacturedOnTest));
            });
        }
    }
}
