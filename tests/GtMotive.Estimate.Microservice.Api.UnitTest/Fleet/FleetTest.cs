using System.Net.Http.Json;
using GtMotive.Estimate.Microservice.Api.Models;
using GtMotive.Estimate.Microservice.Api.UnitTest.Helpers;
using GtMotive.Estimate.Microservice.Application.UseCases.Fleet.AddNewVehicleToFleet;
using GtMotive.Estimate.Microservice.Application.UseCases.Fleet.CreateNewFleet;
using GtMotive.Estimate.Microservice.Application.UseCases.Fleet.GetAvailableVehiclesInFleet;
using GtMotive.Estimate.Microservice.BaseTest.TestHelpers;

namespace GtMotive.Estimate.Microservice.Api.UnitTest.Fleet
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FleetTest"/> class.
    /// </summary>
    [TestFixture]
    public class FleetTest
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
        /// Test to GetAvailableVehicles.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task GetAvailableVehiclesShouldReturnFailBecauseNotExistingFleet()
        {
            // Arrange
            var endpointToConsume =
                UseCasesEndpoints.GetAvailableVehiclesEndpoint
                    .Replace("{fleetId}", BaseTestConstants.FleetIdTest.ToString(), StringComparison.InvariantCultureIgnoreCase);

            // Act
            var result = await _client.GetAsync(new Uri(endpointToConsume, UriKind.Relative));

            // Assert
            Assert.That(result.IsSuccessStatusCode, Is.False);
        }

        /// <summary>
        /// Test to GetAvailableVehicles.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task GetAvailableVehiclesShouldReturnSuccessBecauseExistsFleetWithoutVehicles()
        {
            // Arrange
            var newFleetModel = new NewFleetModel
            {
                FleetName = BaseTestConstants.FleetNameTest
            };
            var newFleetEndpoint = UseCasesEndpoints.NewFleetEndpoint;
            var newFleetResponse = await _client.PostAsJsonAsync(
                new Uri(newFleetEndpoint, UriKind.Relative),
                newFleetModel);
            var newFleetResult = await newFleetResponse.Content.ReadFromJsonAsync<CreateNewFleetResponse>();

            var endpointToConsume =
                UseCasesEndpoints.GetAvailableVehiclesEndpoint
                    .Replace("{fleetId}", newFleetResult!.Fleet!.FleetId.ToString(), StringComparison.InvariantCultureIgnoreCase);

            // Act
            var resultResponse = await _client.GetAsync(new Uri(endpointToConsume, UriKind.Relative));
            var result = await resultResponse.Content.ReadFromJsonAsync<GetAvailableVehiclesInFleetResponse>();

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(resultResponse.IsSuccessStatusCode, Is.True);
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.Vehicles, Is.Empty);
            }
        }

        /// <summary>
        /// Test to GetAvailableVehicles.
        /// </summary>
        /// <returns>Task.</returns>
        [Test]
        public async Task GetAvailableVehiclesShouldReturnSuccessBecauseExistsFleetWithOneVehicle()
        {
            // Arrange
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

            var endpointToConsume =
                UseCasesEndpoints.GetAvailableVehiclesEndpoint
                    .Replace("{fleetId}", newFleetResult.Fleet!.FleetId.ToString(), StringComparison.InvariantCultureIgnoreCase);

            // Act
            var resultResponse = await _client.GetAsync(new Uri(endpointToConsume, UriKind.Relative));
            var result = await resultResponse.Content.ReadFromJsonAsync<GetAvailableVehiclesInFleetResponse>();

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(resultResponse.IsSuccessStatusCode, Is.True);
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.Vehicles, Is.Not.Empty);
                Assert.That(result.Vehicles!.First().VehicleId, Is.EqualTo(addNewVehicleResult!.Fleet!.Vehicles!.First().VehicleId));
            }
        }
    }
}
