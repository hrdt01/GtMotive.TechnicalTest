using GtMotive.Estimate.Microservice.Domain.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Database
{
    /// <inheritdoc />
    public class FleetContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FleetContext"/> class.
        /// </summary>
        public FleetContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "fleet.db");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetContext"/> class.
        /// </summary>
        /// <param name="options">DbContext options.</param>
        public FleetContext(DbContextOptions<FleetContext> options)
            : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "fleet.db");
        }

        /// <summary>
        /// Gets or sets Fleet db set.
        /// </summary>
        public DbSet<Fleet> Fleet { get; set; }

        /// <summary>
        /// Gets or sets Vehicles db set.
        /// </summary>
        public DbSet<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Gets or sets Customers db set.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Gets or sets Rented Vehicles db set.
        /// </summary>
        public DbSet<RentedVehicle> RentedVehicles { get; set; }

        /// <summary>
        /// Gets or sets Fleet Vehicles db set.
        /// </summary>
        public DbSet<FleetVehicle> FleetVehicles { get; set; }

        /// <summary>
        /// Gets the path to SQLite3 database.
        /// </summary>
        public string DbPath { get; }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder), $"The {nameof(optionsBuilder)} are missing to build the db context");
            }

            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);

            modelBuilder
                .Entity<Fleet>(entity =>
            {
                entity.ToTable(nameof(Fleet));

                entity.HasKey(e => e.FleetId);
            })
                .Entity<Customer>(entity =>
            {
                entity.ToTable(nameof(Customer));

                entity.HasKey(e => e.CustomerId);
            })
                .Entity<FleetVehicle>(entity =>
            {
                entity.ToTable(nameof(FleetVehicle));

                entity.HasKey(e => e.FleetVehicleId);
            })
                .Entity<Vehicle>(entity =>
            {
                entity.ToTable(nameof(Vehicle));

                entity.HasKey(e => e.VehicleId);
            })
                .Entity<RentedVehicle>(entity =>
            {
                entity.ToTable(nameof(RentedVehicle));

                entity.HasKey(e => e.RentedVehicleId);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
