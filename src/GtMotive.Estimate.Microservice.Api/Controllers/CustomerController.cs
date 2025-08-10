using System.Net.Mime;
using Asp.Versioning;
using GtMotive.Estimate.Microservice.Api.Models;
using GtMotive.Estimate.Microservice.Application.UseCases.Customer.CreateNewCustomer;
using GtMotive.Estimate.Microservice.Application.UseCases.Customer.RentVehicle;
using GtMotive.Estimate.Microservice.Application.UseCases.Customer.ReturnRentedVehicle;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <inheritdoc />
    [ApiController]
    [ApiVersion("1.0")]
    [Route("customer")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomerController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="mediator"><see cref="IMediator"/> instance.</param>
        /// <param name="logger"><see cref="ILogger{TCategoryName}"/> instance.</param>
        public CustomerController(IMediator mediator, ILogger<CustomerController> logger)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            ArgumentNullException.ThrowIfNull(logger);

            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Process the request to create a new customer.
        /// </summary>
        /// <param name="model"><see cref="NewCustomerModel"/> instance supplied in the request.</param>
        /// <returns><see cref="IResult"/> instance.</returns>
        [HttpPost]
        [Route("newcustomer", Name = "Create a new customer")]
        public async Task<IResult> CreateNewCustomer([FromBody] NewCustomerModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            _logger.LogInfoProcessRequest($"{nameof(CustomerController)} - ", $"{nameof(CreateNewCustomer)}");
            var request = new CreateNewCustomerRequest { CustomerName = model.CustomerName };

            var response = await _mediator.Send(request);
            return Results.Ok(response);
        }

        /// <summary>
        /// Process the request to rent a vehicle.
        /// </summary>
        /// <param name="model"><see cref="RentVehicleModel"/> instance supplied in the request.</param>
        /// <returns><see cref="IResult"/> instance.</returns>
        [HttpPost]
        [Route("rentvehicle", Name = "Rent a vehicle")]
        public async Task<IResult> RentVehicle([FromBody] RentVehicleModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            _logger.LogInfoProcessRequest($"{nameof(CustomerController)} - ", $"{nameof(RentVehicle)}");
            var request = new RentVehicleRequest
            {
                CustomerId = model.CustomerId.ToString(),
                VehicleId = model.VehicleId.ToString(),
                FleetId = model.FleetId.ToString(),
                StartRent = model.StartRent,
                EndRent = model.EndRent
            };

            var response = await _mediator.Send(request);
            return Results.Ok(response);
        }

        /// <summary>
        /// Process the return of a rented vehicle.
        /// </summary>
        /// <param name="model"><see cref="RentVehicleModel"/> instance supplied in the request.</param>
        /// <returns><see cref="IResult"/> instance.</returns>
        [HttpPost]
        [Route("returnrentedvehicle", Name = "Return a rented a vehicle")]
        public async Task<IResult> ReturnRentedVehicle([FromBody] ReturnRentedVehicleModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            _logger.LogInfoProcessRequest($"{nameof(CustomerController)} - ", $"{nameof(ReturnRentedVehicle)}");
            var request = new ReturnRentedVehicleRequest
            {
                RentedVehicleId = model.RentedVehicleId.ToString(),
                CustomerId = model.CustomerId.ToString()
            };

            var response = await _mediator.Send(request);
            return Results.Ok(response);
        }
    }
}
