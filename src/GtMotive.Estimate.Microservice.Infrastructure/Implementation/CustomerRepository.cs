using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Database;
using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Implementation
{
    /// <inheritdoc />
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FleetContext _fleetContext;
        private readonly IRentedVehicleEntityFactory _rentedVehicleEntityFactory;
        private readonly ICustomerEntityFactory _customerEntityFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="fleetContext">DB context.</param>
        /// <param name="rentedVehicleEntityFactory">Instance of <see cref="IRentedVehicleEntityFactory"/>.</param>
        /// <param name="customerEntityFactory">Instance of <see cref="ICustomerEntityFactory"/>.</param>
        public CustomerRepository(
            FleetContext fleetContext,
            IRentedVehicleEntityFactory rentedVehicleEntityFactory,
            ICustomerEntityFactory customerEntityFactory)
        {
            ArgumentNullException.ThrowIfNull(fleetContext);
            ArgumentNullException.ThrowIfNull(rentedVehicleEntityFactory);
            ArgumentNullException.ThrowIfNull(customerEntityFactory);

            _fleetContext = fleetContext;
            _fleetContext.Database.EnsureCreated();
            _rentedVehicleEntityFactory = rentedVehicleEntityFactory;
            _customerEntityFactory = customerEntityFactory;
        }

        /// <inheritdoc />
        public async Task<CustomerDto?> AddNewCustomer(CustomerDto newCustomer)
        {
            ArgumentNullException.ThrowIfNull(newCustomer);

            var customerDbInstance = newCustomer.ToDbEntity(_customerEntityFactory);
            await _fleetContext.Customers.AddAsync(customerDbInstance);
            await _fleetContext.SaveChangesAsync();

            return await GetCustomerById(customerDbInstance.CustomerId);
        }

        /// <inheritdoc />
        public async Task<RentedVehicleDto?> RentVehicle(RentedVehicleDto sourceRentedVehicle)
        {
            ArgumentNullException.ThrowIfNull(sourceRentedVehicle);

            var newRentedVehicleDbInstance = sourceRentedVehicle.ToDbEntity(_rentedVehicleEntityFactory);
            await _fleetContext.RentedVehicles.AddAsync(newRentedVehicleDbInstance);

            await _fleetContext.SaveChangesAsync();

            return await GetRentedVehicleById(newRentedVehicleDbInstance.RentedVehicleId);
        }

        /// <inheritdoc />
        public async Task<RentedVehicleDto?> ReturnRentedVehicle(Guid rentedVehicleId)
        {
            ArgumentNullException.ThrowIfNull(rentedVehicleId);

            var rentedVehicleDto = await GetRentedVehicleById(rentedVehicleId);
            if (rentedVehicleDto == null)
            {
                ArgumentNullException.ThrowIfNull(rentedVehicleDto);
            }

            var rentedVehicleDbEntity = rentedVehicleDto.ToDbEntity(_rentedVehicleEntityFactory);

            rentedVehicleDbEntity.RentFinishedOn = DateTime.UtcNow;

            _fleetContext.RentedVehicles.Update(rentedVehicleDbEntity);

            await _fleetContext.SaveChangesAsync();

            return await GetRentedVehicleById(rentedVehicleId);
        }

        /// <inheritdoc />
        public async Task<RentedVehicleDto?> GetRentedVehicleByIdAndCustomerId(Guid rentedVehicleId, Guid customerId)
        {
            var fromDb = await _fleetContext.RentedVehicles.AsNoTracking()
                .OrderBy(rentedVehicle => rentedVehicle.RentedVehicleId)
                .Where(rentedVehicle =>
                    rentedVehicle.RentedVehicleId == rentedVehicleId
                    && rentedVehicle.CustomerId == customerId)
                .FirstOrDefaultAsync();

            return fromDb == null
                ? null
                : new RentedVehicleDto
                {
                    RentedVehicleId = fromDb.RentedVehicleId,
                    FleetId = fromDb.FleetId,
                    VehicleId = fromDb.VehicleId,
                    CustomerId = fromDb.CustomerId,
                    StartRent = fromDb.RentStartedOn,
                    EndRent = fromDb.RentFinishedOn
                };
        }

        /// <inheritdoc />
        public async Task<RentedVehicleDto?> GetRentedVehicleById(Guid rentedVehicleId)
        {
            var fromDb = await _fleetContext.RentedVehicles.AsNoTracking()
                .OrderBy(rentedVehicle => rentedVehicle.RentedVehicleId)
                .Where(rentedVehicle => rentedVehicle.RentedVehicleId == rentedVehicleId)
                .FirstOrDefaultAsync();

            return fromDb == null
                ? null
                : new RentedVehicleDto
                {
                    RentedVehicleId = fromDb.RentedVehicleId,
                    FleetId = fromDb.FleetId,
                    VehicleId = fromDb.VehicleId,
                    CustomerId = fromDb.CustomerId,
                    StartRent = fromDb.RentStartedOn,
                    EndRent = fromDb.RentFinishedOn
                };
        }

        /// <inheritdoc />
        public async Task<CustomerDto?> GetCustomerById(Guid customerId)
        {
            var fromDb = await _fleetContext.Customers.AsNoTracking()
                .OrderBy(customer => customer.CustomerId)
                .Where(customer => customer.CustomerId == customerId)
                .FirstOrDefaultAsync();

            return fromDb == null
                ? null
                : new CustomerDto
                {
                    CustomerId = fromDb.CustomerId,
                    CustomerName = fromDb.CustomerName
                };
        }
    }
}
