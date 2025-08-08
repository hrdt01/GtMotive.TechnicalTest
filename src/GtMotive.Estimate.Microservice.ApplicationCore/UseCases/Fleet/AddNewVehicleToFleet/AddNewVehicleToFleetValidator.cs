using FluentValidation;
using GtMotive.Estimate.Microservice.Application.UseCases.Validators;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Fleet.AddNewVehicleToFleet
{
    /// <inheritdoc />
    public class AddNewVehicleToFleetValidator : AbstractValidator<AddNewVehicleToFleetRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddNewVehicleToFleetValidator"/> class.
        /// </summary>
        public AddNewVehicleToFleetValidator()
        {
            Include(new SuppliedFleetIdInRequestValidator<AddNewVehicleToFleetRequest>());
            RuleFor(req => req.VehicleBrand).NotEmpty().WithMessage("Please enter a value.");
            RuleFor(req => req.VehicleModel).NotEmpty().WithMessage("Please enter a value.");
            RuleFor(req => req.VehicleManufacturedOn).Must(BeValidDate).WithMessage("Please enter a valid date.");
        }

        private bool BeValidDate(DateTime arg)
        {
            return arg != DateTime.MinValue && arg != DateTime.MaxValue;
        }
    }
}
