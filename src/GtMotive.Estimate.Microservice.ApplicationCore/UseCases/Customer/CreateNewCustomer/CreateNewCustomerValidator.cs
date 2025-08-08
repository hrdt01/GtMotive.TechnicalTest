using FluentValidation;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Customer.CreateNewCustomer
{
    /// <inheritdoc />
    public class CreateNewCustomerValidator : AbstractValidator<CreateNewCustomerRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewCustomerValidator"/> class.
        /// </summary>
        public CreateNewCustomerValidator()
        {
            RuleFor(req => req.CustomerName).NotEmpty().WithMessage("Please enter a value.");
        }
    }
}
