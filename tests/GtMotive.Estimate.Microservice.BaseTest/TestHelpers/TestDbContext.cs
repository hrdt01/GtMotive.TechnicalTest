using GtMotive.Estimate.Microservice.Domain.DbEntities;
using GtMotive.Estimate.Microservice.Infrastructure.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.BaseTest.TestHelpers
{
    /// <summary>
    /// TestDbContext definition.
    /// </summary>
    public static class TestDbContext
    {
        /// <summary>
        /// Method to create a Sqlite connection for testing purposes.
        /// </summary>
        /// <returns>SqliteConnection object for testing.</returns>
        public static SqliteConnection CreateSqliteTestConnection()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Method to create options for the provided DbContext.
        /// </summary>
        /// <typeparam name="T">Type of DbContext.</typeparam>
        /// <param name="sqliteConnection">SqliteConnection to provide options for.</param>
        /// <returns>DbContextOptions object with options for the provided DbContext.</returns>
        public static DbContextOptions<T> CreateOptions<T>(SqliteConnection sqliteConnection)
            where T : DbContext
            => new DbContextOptionsBuilder<T>()
                .UseSqlite(sqliteConnection)
                .EnableSensitiveDataLogging(true)
                .Options;

        /// <summary>
        /// Method to create a DbContext for the provided DbContext and connection.
        /// </summary>
        /// <typeparam name="T">Type of DbContext.</typeparam>
        /// <param name="sqliteConnection">SqliteConnection to provide context for.</param>
        /// <returns>New instance of the provided DbContext connected to the provided SqliteConnection.</returns>
        public static T? CreateContext<T>(SqliteConnection sqliteConnection)
            where T : DbContext
        {
            var dbContextOptions = CreateOptions<T>(sqliteConnection);
            var instance = Activator.CreateInstance(typeof(T), dbContextOptions);
            if (instance != null)
            {
                var context = (T)instance;
                context.Database.EnsureCreated();

                return context;
            }

            return null;
        }

        /// <summary>
        /// Cleaning up database.
        /// </summary>
        public static void DatabaseCleanup()
        {
            using var testDbConnection = CreateSqliteTestConnection();
            using var testDbContext = CreateContext<FleetContext>(testDbConnection);
            testDbContext!.Vehicles.RemoveRange(
                testDbContext.Vehicles.Where(vehicle => vehicle.VehicleId == BaseTestConstants.VehicleIdTest));
            testDbContext.Fleet.RemoveRange(
                testDbContext.Fleet.Where(fleet => fleet.FleetName == BaseTestConstants.FleetNameTest));
            testDbContext.Customers.RemoveRange(
                testDbContext.Customers.Where(customer => customer.CustomerName == BaseTestConstants.CustomerNameTest));

            testDbContext.SaveChanges();
        }

        /// <summary>
        /// Seeding minimum data to rent vehicle.
        /// </summary>
        public static void SeedDataToRentVehicle()
        {
            var initialFleet = new Fleet
            {
                FleetName = BaseTestConstants.FleetNameTest,
                FleetId = BaseTestConstants.FleetIdTest
            };
            var initialVehicle = new Vehicle
            {
                Brand = BaseTestConstants.BrandNameTest,
                Model = BaseTestConstants.ModelNameTest,
                ManufacturedOn = BaseTestConstants.ManufacturedOnTest,
                VehicleId = BaseTestConstants.VehicleIdTest
            };
            var initialFleetVehicle = new FleetVehicle
            {
                FleetId = initialFleet.FleetId,
                VehicleId = initialVehicle.VehicleId
            };
#pragma warning disable IDE0028
            initialFleet.FleetVehicles = new List<FleetVehicle> { initialFleetVehicle };
#pragma warning restore IDE0028

            using var testDbConnection = CreateSqliteTestConnection();
            using var testDbContext = CreateContext<FleetContext>(testDbConnection);
            testDbContext!.Vehicles.Add(initialVehicle);
            testDbContext.Fleet.Add(initialFleet);
            testDbContext.SaveChanges();
        }

        /// <summary>
        /// Seeding minimum data to return rented vehicle.
        /// </summary>
        public static void SeedDataToReturnRentedVehicle()
        {
            var initialCustomer = new Customer
            {
                CustomerName = BaseTestConstants.CustomerNameTest,
                CustomerId = BaseTestConstants.CustomerIdTest
            };
            var initialFleet = new Fleet
            {
                FleetName = BaseTestConstants.FleetNameTest,
                FleetId = BaseTestConstants.FleetIdTest
            };
            var initialVehicle = new Vehicle
            {
                Brand = BaseTestConstants.BrandNameTest,
                Model = BaseTestConstants.ModelNameTest,
                ManufacturedOn = BaseTestConstants.ManufacturedOnTest,
                VehicleId = BaseTestConstants.VehicleIdTest
            };
            var initialFleetVehicle = new FleetVehicle
            {
                FleetId = initialFleet.FleetId,
                VehicleId = initialVehicle.VehicleId
            };
            var initialRentedVehicle = new RentedVehicle
            {
                FleetId = initialFleet.FleetId,
                VehicleId = initialVehicle.VehicleId,
                CustomerId = initialCustomer.CustomerId,
                RentStartedOn = BaseTestConstants.RentStartedOn,
                RentFinishedOn = BaseTestConstants.RentFinishedOn,
                RentedVehicleId = BaseTestConstants.RentedVehicleIdTest
            };
#pragma warning disable IDE0028
            initialFleet.FleetVehicles = new List<FleetVehicle> { initialFleetVehicle };
#pragma warning restore IDE0028

            using var testDbConnection = CreateSqliteTestConnection();
            using var testDbContext = CreateContext<FleetContext>(testDbConnection);
            testDbContext!.Customers.Add(initialCustomer);
            testDbContext.Vehicles.Add(initialVehicle);
            testDbContext.Fleet.Add(initialFleet);
            testDbContext.RentedVehicles.Add(initialRentedVehicle);
            testDbContext.SaveChanges();
        }
    }
}
