using FluentValidation;
using GtMotive.Estimate.Microservice.Application.UseCases.Validators;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Customer.RentVehicle
{
    /// <inheritdoc />
    public class RentVehicleValidator : AbstractValidator<RentVehicleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleValidator"/> class.
        /// </summary>
        public RentVehicleValidator()
        {
            Include(new SuppliedFleetIdInRequestValidator<RentVehicleRequest>());
            RuleFor(req => req.CustomerId).NotEmpty().WithMessage("Please enter a value.");
            RuleFor(req => req.FleetId).NotEmpty().WithMessage("Please enter a value.");
            RuleFor(req => req.VehicleId).NotEmpty().WithMessage("Please enter a value.");
            RuleFor(req => req.StartRent).Must(BeValidDate).WithMessage("Please enter a valid date.");
            RuleFor(req => req.EndRent).Must(BeValidDate).WithMessage("Please enter a valid date.");
            RuleFor(req => new { req.StartRent, req.EndRent })
                .Must(x => BeValidRentPeriod(x.StartRent, x.EndRent))
                .WithMessage("Wrong Rental period");
        }

        private static bool BeValidRentPeriod(DateTime startRent, DateTime endRent)
        {
            return endRent > startRent;
        }

        private static bool BeValidDate(DateTime arg)
        {
            return arg != DateTime.MinValue && arg != DateTime.MaxValue;
        }
    }
}
