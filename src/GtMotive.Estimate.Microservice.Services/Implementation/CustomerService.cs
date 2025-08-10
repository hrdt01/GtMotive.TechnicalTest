using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GtMotive.Estimate.Microservice.Services.Implementation
{
    /// <inheritdoc />
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IFleetRepository _fleetRepository;
        private readonly ILogger<CustomerService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="customerRepository">Instance of <see cref="ICustomerRepository"/>.</param>
        /// <param name="fleetRepository">Instance of <see cref="IFleetRepository"/>.</param>
        /// <param name="logger">Instance of <see cref="ILogger"/>.</param>
        public CustomerService(
            ICustomerRepository customerRepository,
            IFleetRepository fleetRepository,
            ILogger<CustomerService> logger)
        {
            ArgumentNullException.ThrowIfNull(customerRepository);
            ArgumentNullException.ThrowIfNull(fleetRepository);
            ArgumentNullException.ThrowIfNull(logger);

            _customerRepository = customerRepository;
            _fleetRepository = fleetRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<RentedVehicleDto?> RentVehicle(RentedVehicleDto source)
        {
            ArgumentNullException.ThrowIfNull(source);
            var availableVehicles =
                await _fleetRepository.GetAvailableFleetVehicles(source.FleetId);
            var isAvailable = availableVehicles.Any(vehicle => vehicle.VehicleId == source.VehicleId);
            return !isAvailable
                ? null
                : await _customerRepository.RentVehicle(source);
        }

        /// <inheritdoc />
        public async Task<RentedVehicleDto?> ReturnRentedVehicle(RentedVehicleDto rentedVehicle)
        {
            ArgumentNullException.ThrowIfNull(rentedVehicle);

            var existingRentedVehicle =
                await _customerRepository.GetRentedVehicleByIdAndCustomerId(
                    rentedVehicle.RentedVehicleId,
                    rentedVehicle.CustomerId);

            if (existingRentedVehicle == null)
            {
                _logger.LogWarningNotFoundRentedVehicle(
                    $"{nameof(CustomerService)} - {nameof(ReturnRentedVehicle)} - ",
                    rentedVehicle.RentedVehicleId.ToString());

                ArgumentNullException.ThrowIfNull(existingRentedVehicle);
            }

            return await _customerRepository.ReturnRentedVehicle(rentedVehicle.RentedVehicleId);
        }

        /// <inheritdoc />
        public async Task<CustomerDto?> AddNewCustomer(string newCustomerName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(newCustomerName);

            var customerDto = new CustomerDto { CustomerName = newCustomerName };

            return await _customerRepository.AddNewCustomer(customerDto);
        }
    }
}
