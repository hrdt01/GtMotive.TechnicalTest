using FluentValidation;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Customer.ReturnRentedVehicle
{
    /// <inheritdoc />
    public class ReturnRentedVehicleValidator : AbstractValidator<ReturnRentedVehicleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnRentedVehicleValidator"/> class.
        /// </summary>
        public ReturnRentedVehicleValidator()
        {
            RuleFor(req => req.RentedVehicleId).NotEmpty().WithMessage("Please enter a value.");
            RuleFor(req => req.CustomerId).NotEmpty().WithMessage("Please enter a value.");
        }
    }
}
