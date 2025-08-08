using FluentValidation;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Validators
{
    /// <inheritdoc />
    public class SuppliedFleetIdInRequestValidator<TRequest> : AbstractValidator<TRequest>
        where TRequest : class, IFleetRequestContract
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuppliedFleetIdInRequestValidator{TRequest}"/> class.
        /// </summary>
        public SuppliedFleetIdInRequestValidator()
        {
            RuleFor(req => req.FleetId).NotEmpty();
        }
    }
}
