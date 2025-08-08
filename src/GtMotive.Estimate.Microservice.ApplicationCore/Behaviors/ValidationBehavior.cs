using FluentValidation;
using MediatR;

namespace GtMotive.Estimate.Microservice.Application.Behaviors
{
    /// <summary>
    /// ValidationBehavior definition.
    /// </summary>
    /// <typeparam name="TRequest">Request processed by MediatR handlers.</typeparam>
    /// <typeparam name="TResponse">Response offered by MediatR handlers.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly bool _hasValidators;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">Collection of validators.</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
            _hasValidators = _validators.Any();
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(next);

            if (!_hasValidators)
            {
                return await next(cancellationToken);
            }

            var context = new ValidationContext<TRequest>(request);

            var validationResults =
                await Task.WhenAll(_validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var failures =
                validationResults.SelectMany(r => r.Errors)
                    .Where(f => f != null).ToList();

            return failures.Count > 0
                ? throw new ValidationException(
                    "One or more validation errors occurred", failures)
                : await next(cancellationToken);
        }
    }
}
