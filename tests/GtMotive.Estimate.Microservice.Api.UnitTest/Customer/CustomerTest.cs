using System.Net.Http.Json;
using GtMotive.Estimate.Microservice.Api.Models;
using GtMotive.Estimate.Microservice.Api.UnitTest.Helpers;
using GtMotive.Estimate.Microservice.Application.UseCases.Customer.CreateNewCustomer;
using GtMotive.Estimate.Microservice.Application.UseCases.Customer.RentVehicle;
using GtMotive.Estimate.Microservice.Application.UseCases.Customer.ReturnRentedVehicle;
using GtMotive.Estimate.Microservice.Application.UseCases.Fleet.AddNewVehicleToFleet;
using GtMotive.Estimate.Microservice.Application.UseCases.Fleet.CreateNewFleet;
using GtMotive.Estimate.Microservice.Application.UseCases.Fleet.GetAvailableVehiclesInFleet;
using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;

namespace GtMotive.Estimate.Microservice.Api.UnitTest.Customer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerTest"/> class.
    /// </summary>
    [TestFixture]
    public class CustomerTest
    {
        private WebApplicationFixture _factory;
        private HttpClient _client;

        /// <summary>
        /// Method to allocate resources.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new WebApplicationFixture();
        }

        /// <summary>
        /// Method to free resources.
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _factory.Dispose();
        }

        /// <summary>
        /// Method to allocate resources.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _client = _factory.CreateClient();
        }

        /// <summary>
        /// Method to free resources.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        /// <summary>
        /// Test to CreateNewCustomer.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task CreateNewCustomerShouldReturnFailBecauseCustomerWithoutName()
        {
            // Arrange
            var newCustomerModel = new NewCustomerModel
            {
                CustomerName = string.Empty
            };
            var newCustomerEndpoint = UseCasesEndpoints.NewCustomerEndpoint;

            // Act
            var newCustomerResponse = await _client.PostAsJsonAsync(
                new Uri(newCustomerEndpoint, UriKind.Relative),
                newCustomerModel);

            // Assert
            Assert.That(newCustomerResponse.IsSuccessStatusCode, Is.False);
        }

        /// <summary>
        /// Test to CreateNewCustomer.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task CreateNewCustomerShouldReturnSuccess()
        {
            // Arrange
            var newCustomerModel = new NewCustomerModel
            {
                CustomerName = BaseTestConstants.CustomerNameTest
            };
            var newCustomerEndpoint = UseCasesEndpoints.NewCustomerEndpoint;

            // Act
            var newCustomerResponse = await _client.PostAsJsonAsync(
                new Uri(newCustomerEndpoint, UriKind.Relative),
                newCustomerModel);
            var newCustomerResult = await newCustomerResponse.Content.ReadFromJsonAsync<CreateNewCustomerResponse>();

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(newCustomerResponse.IsSuccessStatusCode, Is.True);
                Assert.That(newCustomerResult, Is.Not.Null);
                Assert.That(newCustomerResult!.Customer, Is.Not.Null);
                Assert.That(newCustomerResult.Customer!.CustomerName, Is.EqualTo(newCustomerModel.CustomerName));
                Assert.That(newCustomerResult.Customer!.RentedVehicles, Is.Null);
            }
        }

        /// <summary>
        /// Test to RentVehicle.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task RentVehicleShouldReturnSuccess()
        {
            // Arrange
            var newCustomerModel = new NewCustomerModel
            {
                CustomerName = BaseTestConstants.CustomerNameTest
            };
            var newCustomerEndpoint = UseCasesEndpoints.NewCustomerEndpoint;
            var newCustomerResponse = await _client.PostAsJsonAsync(
                new Uri(newCustomerEndpoint, UriKind.Relative),
                newCustomerModel);
            var newCustomerResult = await newCustomerResponse.Content.ReadFromJsonAsync<CreateNewCustomerResponse>();

            var newFleetModel = new NewFleetModel
            {
                FleetName = BaseTestConstants.FleetNameTest
            };
            var newFleetEndpoint = UseCasesEndpoints.NewFleetEndpoint;
            var newFleetResponse = await _client.PostAsJsonAsync(
                new Uri(newFleetEndpoint, UriKind.Relative),
                newFleetModel);
            var newFleetResult = await newFleetResponse.Content.ReadFromJsonAsync<CreateNewFleetResponse>();

            var newVehicleModel = new AddNewVehicleModel()
            {
                FleetId = newFleetResult!.Fleet!.FleetId,
                VehicleBrand = BaseTestConstants.BrandNameTest,
                VehicleModel = BaseTestConstants.ModelNameTest,
                VehicleManufacturedOn = BaseTestConstants.ManufacturedOnTest
            };
            var addNewVehicleEndpoint = UseCasesEndpoints.AddNewVehicleEndpoint;
            var addNewVehicleResponse = await _client.PostAsJsonAsync(
                new Uri(addNewVehicleEndpoint, UriKind.Relative),
                newVehicleModel);
            var addNewVehicleResult = await addNewVehicleResponse.Content.ReadFromJsonAsync<AddNewVehicleToFleetResponse>();

            var rentVehicleModel = new RentVehicleModel()
            {
                FleetId = newFleetResult.Fleet.FleetId,
                CustomerId = newCustomerResult!.Customer!.CustomerId,
                VehicleId = addNewVehicleResult!.Fleet!.Vehicles!.First().VehicleId,
                StartRent = BaseTestConstants.RentStartedOn,
                EndRent = BaseTestConstants.RentFinishedOn
            };
            var rentVehicleEndpoint = UseCasesEndpoints.RentVehicleEndpoint;

            // Act
            var rentVehicleResponse = await _client.PostAsJsonAsync(
                new Uri(rentVehicleEndpoint, UriKind.Relative),
                rentVehicleModel);
            var rentVehicleResult = await rentVehicleResponse.Content.ReadFromJsonAsync<RentVehicleResponse>();

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(rentVehicleResponse.IsSuccessStatusCode, Is.True);
                Assert.That(rentVehicleResult, Is.Not.Null);
                Assert.That(rentVehicleResult!.RentedVehicle, Is.Not.Null);
                Assert.That(rentVehicleResult.RentedVehicle!.CustomerId, Is.EqualTo(newCustomerResult.Customer.CustomerId));
            }

            // Get available vehicles from fleet to check there's no available vehicles
            var endpointToConsume =
                UseCasesEndpoints.GetAvailableVehiclesEndpoint
                    .Replace("{fleetId}", rentVehicleResult.RentedVehicle!.FleetId.ToString(), StringComparison.InvariantCultureIgnoreCase);
            var availableVehiclesResponse = await _client.GetAsync(new Uri(endpointToConsume, UriKind.Relative));
            var availableVehiclesResult = await availableVehiclesResponse.Content.ReadFromJsonAsync<GetAvailableVehiclesInFleetResponse>();
            using (Assert.EnterMultipleScope())
            {
                Assert.That(availableVehiclesResponse.IsSuccessStatusCode, Is.True);
                Assert.That(availableVehiclesResult, Is.Not.Null);
                Assert.That(availableVehiclesResult!.Vehicles, Is.Empty);
            }
        }

        /// <summary>
        /// Test to RentVehicle.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task RentVehicleShouldReturnFailBecauseCustomerAlreadyHasARentedVehicle()
        {
            // Arrange
            var newCustomerModel = new NewCustomerModel
            {
                CustomerName = BaseTestConstants.CustomerNameTest
            };
            var newCustomerEndpoint = UseCasesEndpoints.NewCustomerEndpoint;
            var newCustomerResponse = await _client.PostAsJsonAsync(
                new Uri(newCustomerEndpoint, UriKind.Relative),
                newCustomerModel);
            var newCustomerResult = await newCustomerResponse.Content.ReadFromJsonAsync<CreateNewCustomerResponse>();

            var newFleetModel = new NewFleetModel
            {
                FleetName = BaseTestConstants.FleetNameTest
            };
            var newFleetEndpoint = UseCasesEndpoints.NewFleetEndpoint;
            var newFleetResponse = await _client.PostAsJsonAsync(
                new Uri(newFleetEndpoint, UriKind.Relative),
                newFleetModel);
            var newFleetResult = await newFleetResponse.Content.ReadFromJsonAsync<CreateNewFleetResponse>();

            var newVehicleModel1 = new AddNewVehicleModel()
            {
                FleetId = newFleetResult!.Fleet!.FleetId,
                VehicleBrand = BaseTestConstants.BrandNameTest,
                VehicleModel = BaseTestConstants.ModelNameTest,
                VehicleManufacturedOn = BaseTestConstants.ManufacturedOnTest
            };
            var addNewVehicleEndpoint = UseCasesEndpoints.AddNewVehicleEndpoint;
            var addNewVehicle1Response = await _client.PostAsJsonAsync(
                new Uri(addNewVehicleEndpoint, UriKind.Relative),
                newVehicleModel1);
            var addNewVehicle1Result = await addNewVehicle1Response.Content.ReadFromJsonAsync<AddNewVehicleToFleetResponse>();

            var newVehicleModel2 = new AddNewVehicleModel()
            {
                FleetId = newFleetResult.Fleet!.FleetId,
                VehicleBrand = BaseTestConstants.BrandNameTest,
                VehicleModel = BaseTestConstants.AnotherModelNameTest,
                VehicleManufacturedOn = BaseTestConstants.ManufacturedOnTest
            };
            var addNewVehicle2Response = await _client.PostAsJsonAsync(
                new Uri(addNewVehicleEndpoint, UriKind.Relative),
                newVehicleModel2);
            var addNewVehicle2Result = await addNewVehicle2Response.Content.ReadFromJsonAsync<AddNewVehicleToFleetResponse>();

            var rentVehicleModel1 = new RentVehicleModel()
            {
                FleetId = newFleetResult.Fleet.FleetId,
                CustomerId = newCustomerResult!.Customer!.CustomerId,
                VehicleId =
                    addNewVehicle1Result!.Fleet!.Vehicles!
                        .First(x => x.Model!.Equals(BaseTestConstants.ModelNameTest, StringComparison.OrdinalIgnoreCase)).VehicleId,
                StartRent = BaseTestConstants.RentStartedOn,
                EndRent = BaseTestConstants.RentFinishedOn
            };
            var rentVehicleEndpoint = UseCasesEndpoints.RentVehicleEndpoint;

            var rentVehicle1Response = await _client.PostAsJsonAsync(
                new Uri(rentVehicleEndpoint, UriKind.Relative),
                rentVehicleModel1);
            var rentVehicle1Result = await rentVehicle1Response.Content.ReadFromJsonAsync<RentVehicleResponse>();

            var rentVehicleModel2 = new RentVehicleModel()
            {
                FleetId = newFleetResult.Fleet.FleetId,
                CustomerId = newCustomerResult.Customer!.CustomerId,
                VehicleId =
                    addNewVehicle2Result!.Fleet!.Vehicles!
                        .First(x => x.Model!.Equals(BaseTestConstants.AnotherModelNameTest, StringComparison.OrdinalIgnoreCase)).VehicleId,
                StartRent = BaseTestConstants.RentStartedOn,
                EndRent = BaseTestConstants.RentFinishedOn
            };

            // Act
            var rentVehicle2Response = await _client.PostAsJsonAsync(
                new Uri(rentVehicleEndpoint, UriKind.Relative),
                rentVehicleModel2);
            var rentVehicle2Result = await rentVehicle2Response.Content.ReadFromJsonAsync<RentVehicleResponse>();

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(rentVehicle1Response.IsSuccessStatusCode, Is.True);
                Assert.That(rentVehicle1Result, Is.Not.Null);
                Assert.That(rentVehicle1Result!.RentedVehicle, Is.Not.Null);
                Assert.That(rentVehicle1Result.RentedVehicle!.CustomerId, Is.EqualTo(newCustomerResult.Customer.CustomerId));
                Assert.That(rentVehicle2Response.IsSuccessStatusCode, Is.True);
                Assert.That(rentVehicle2Result!.RentedVehicle, Is.Null);
            }

            // Get available vehicles from fleet to check there's no available vehicles
            var endpointToConsume =
                UseCasesEndpoints.GetAvailableVehiclesEndpoint
                    .Replace("{fleetId}", rentVehicle1Result.RentedVehicle!.FleetId.ToString(), StringComparison.InvariantCultureIgnoreCase);
            var availableVehiclesResponse = await _client.GetAsync(new Uri(endpointToConsume, UriKind.Relative));
            var availableVehiclesResult = await availableVehiclesResponse.Content.ReadFromJsonAsync<GetAvailableVehiclesInFleetResponse>();
            using (Assert.EnterMultipleScope())
            {
                Assert.That(availableVehiclesResponse.IsSuccessStatusCode, Is.True);
                Assert.That(availableVehiclesResult, Is.Not.Null);
                Assert.That(availableVehiclesResult!.Vehicles, Is.Not.Empty);
            }
        }

        /// <summary>
        /// Test to ReturnRentedVehicle.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task ReturnRentedVehicleShouldReturnSuccess()
        {
            // Arrange
            var newCustomerModel = new NewCustomerModel
            {
                CustomerName = BaseTestConstants.CustomerNameTest
            };
            var newCustomerEndpoint = UseCasesEndpoints.NewCustomerEndpoint;
            var newCustomerResponse = await _client.PostAsJsonAsync(
                new Uri(newCustomerEndpoint, UriKind.Relative),
                newCustomerModel);
            var newCustomerResult = await newCustomerResponse.Content.ReadFromJsonAsync<CreateNewCustomerResponse>();

            var newFleetModel = new NewFleetModel
            {
                FleetName = BaseTestConstants.FleetNameTest
            };
            var newFleetEndpoint = UseCasesEndpoints.NewFleetEndpoint;
            var newFleetResponse = await _client.PostAsJsonAsync(
                new Uri(newFleetEndpoint, UriKind.Relative),
                newFleetModel);
            var newFleetResult = await newFleetResponse.Content.ReadFromJsonAsync<CreateNewFleetResponse>();

            var newVehicleModel = new AddNewVehicleModel()
            {
                FleetId = newFleetResult!.Fleet!.FleetId,
                VehicleBrand = BaseTestConstants.BrandNameTest,
                VehicleModel = BaseTestConstants.ModelNameTest,
                VehicleManufacturedOn = BaseTestConstants.ManufacturedOnTest
            };
            var addNewVehicleEndpoint = UseCasesEndpoints.AddNewVehicleEndpoint;
            var addNewVehicleResponse = await _client.PostAsJsonAsync(
                new Uri(addNewVehicleEndpoint, UriKind.Relative),
                newVehicleModel);
            var addNewVehicleResult = await addNewVehicleResponse.Content.ReadFromJsonAsync<AddNewVehicleToFleetResponse>();

            var rentVehicleModel = new RentVehicleModel()
            {
                FleetId = newFleetResult.Fleet.FleetId,
                CustomerId = newCustomerResult!.Customer!.CustomerId,
                VehicleId = addNewVehicleResult!.Fleet!.Vehicles!.First().VehicleId,
                StartRent = BaseTestConstants.RentStartedOn,
                EndRent = BaseTestConstants.RentFinishedOn
            };
            var rentVehicleEndpoint = UseCasesEndpoints.RentVehicleEndpoint;
            var rentVehicleResponse = await _client.PostAsJsonAsync(
                new Uri(rentVehicleEndpoint, UriKind.Relative),
                rentVehicleModel);
            var rentVehicleResult = await rentVehicleResponse.Content.ReadFromJsonAsync<RentVehicleResponse>();

            var returnRentedVehicleEndpoint = UseCasesEndpoints.ReturnRentedVehicleEndpoint;
            var returnRentedVehicleModel = new ReturnRentedVehicleModel()
            {
                RentedVehicleId = rentVehicleResult!.RentedVehicle!.RentedVehicleId,
                CustomerId = rentVehicleResult.RentedVehicle!.CustomerId
            };

            // Act
            var returnRentedVehicleResponse = await _client.PostAsJsonAsync(
                new Uri(returnRentedVehicleEndpoint, UriKind.Relative),
                returnRentedVehicleModel);
            var returnRentedVehicleResult =
                await returnRentedVehicleResponse.Content.ReadFromJsonAsync<ReturnRentedVehicleResponse>();

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(returnRentedVehicleResponse.IsSuccessStatusCode, Is.True);
                Assert.That(returnRentedVehicleResult, Is.Not.Null);
                Assert.That(returnRentedVehicleResult!.RentedVehicle!.RentedVehicleId, Is.EqualTo(rentVehicleResult.RentedVehicle.RentedVehicleId));
                Assert.That(returnRentedVehicleResult.RentedVehicle!.EndRent, Is.Not.EqualTo(rentVehicleResult.RentedVehicle.EndRent));
                Assert.That(returnRentedVehicleResult.RentedVehicle!.EndRent, Is.LessThan(DateTime.UtcNow));
            }

            // Get available vehicles from fleet to check there's no available vehicles
            var endpointToConsume =
                UseCasesEndpoints.GetAvailableVehiclesEndpoint
                    .Replace("{fleetId}", rentVehicleResult.RentedVehicle!.FleetId.ToString(), StringComparison.InvariantCultureIgnoreCase);
            var availableVehiclesResponse = await _client.GetAsync(new Uri(endpointToConsume, UriKind.Relative));
            var availableVehiclesResult = await availableVehiclesResponse.Content.ReadFromJsonAsync<GetAvailableVehiclesInFleetResponse>();
            using (Assert.EnterMultipleScope())
            {
                Assert.That(availableVehiclesResponse.IsSuccessStatusCode, Is.True);
                Assert.That(availableVehiclesResult, Is.Not.Null);
                Assert.That(availableVehiclesResult!.Vehicles, Is.Not.Empty);
                Assert.That(
                    availableVehiclesResult.Vehicles!.Any(vehicle =>
                        vehicle.VehicleId == returnRentedVehicleResult.RentedVehicle.VehicleId), Is.True);
            }
        }
    }
}
