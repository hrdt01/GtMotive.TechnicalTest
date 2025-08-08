using FluentValidation;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Fleet.CreateNewFleet
{
    /// <inheritdoc />
    public class CreateNewFleetValidator : AbstractValidator<CreateNewFleetRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewFleetValidator"/> class.
        /// </summary>
        public CreateNewFleetValidator()
        {
            RuleFor(req => req.FleetName).NotEmpty().WithMessage("Please enter a value.");
        }
    }
}
