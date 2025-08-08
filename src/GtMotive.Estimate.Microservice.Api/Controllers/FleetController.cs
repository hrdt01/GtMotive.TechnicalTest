using System.Net.Mime;
using Asp.Versioning;
using GtMotive.Estimate.Microservice.Api.Models;
using GtMotive.Estimate.Microservice.Application.UseCases.Fleet.AddNewVehicleToFleet;
using GtMotive.Estimate.Microservice.Application.UseCases.Fleet.CreateNewFleet;
using GtMotive.Estimate.Microservice.Application.UseCases.Fleet.GetAvailableVehiclesInFleet;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <inheritdoc />
    [ApiController]
    [ApiVersion("1.0")]
    [Route("fleet")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public class FleetController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FleetController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetController"/> class.
        /// </summary>
        /// <param name="mediator"><see cref="IMediator"/> instance.</param>
        /// <param name="logger"><see cref="ILogger{TCategoryName}"/> instance.</param>
        public FleetController(IMediator mediator, ILogger<FleetController> logger)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            ArgumentNullException.ThrowIfNull(logger);

            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Process the request to create a new fleet.
        /// </summary>
        /// <param name="model"><see cref="NewFleetModel"/> instance supplied in the request.</param>
        /// <returns><see cref="IResult"/> instance.</returns>
        [HttpPost]
        [Route("newfleet", Name = "Create a new fleet")]
        public async Task<IResult> CreateNewFleet([FromBody] NewFleetModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            _logger.LogInfoProcessRequest($"{nameof(FleetController)} - ", $"{nameof(CreateNewFleet)}");
            var request = new CreateNewFleetRequest { FleetName = model.FleetName };

            var response = await _mediator.Send(request);
            return Results.Ok(response);
        }

        /// <summary>
        /// Process the request to add a new vehicle to an existing fleet.
        /// </summary>
        /// <param name="model"><see cref="AddNewVehicleModel"/> instance supplied in the request.</param>
        /// <returns><see cref="IResult"/> instance.</returns>
        [HttpPost]
        [Route("addvehicle", Name = "Add a new vehicle to fleet")]
        public async Task<IResult> AddNewVehicleToFleet([FromBody] AddNewVehicleModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            _logger.LogInfoProcessRequest($"{nameof(FleetController)} - ", $"{nameof(AddNewVehicleToFleet)}");
            var request = new AddNewVehicleToFleetRequest
            {
                FleetId = model.FleetId.ToString(),
                VehicleBrand = model.VehicleBrand,
                VehicleModel = model.VehicleModel,
                VehicleManufacturedOn = model.VehicleManufacturedOn
            };

            var response = await _mediator.Send(request);
            return Results.Ok(response);
        }

        /// <summary>
        /// Process the request to obtain the available vehicles in an existing fleet.
        /// </summary>
        /// <param name="fleetId">Fleet identifier supplied in the request.</param>
        /// <returns><see cref="IResult"/> instance.</returns>
        [HttpGet]
        [Route("{fleetId}/availablevehicles", Name = "Get available vehicles belonging to fleet")]
        public async Task<IResult> GetAvailableVehicles(string fleetId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fleetId);

            _logger.LogInfoProcessRequest($"{nameof(FleetController)} - ", $"{nameof(GetAvailableVehicles)}");
            var request = new GetAvailableVehiclesInFleetRequest
            {
                FleetId = fleetId
            };

            var response = await _mediator.Send(request);
            return Results.Ok(response);
        }
    }
}
