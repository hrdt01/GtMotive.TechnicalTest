using FluentValidation;
using GtMotive.Estimate.Microservice.Application.Behaviors;
using GtMotive.Estimate.Microservice.Services.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Application.Extensions
{
    /// <summary>
    /// Extensions register.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add to the DI Service Collection the services needed to work with injected services.
        /// </summary>
        /// <param name="services">Current contract where the services will be added.</param>
        /// <returns>Updated current contract.</returns>
        public static IServiceCollection AddApplicationArtifacts(this IServiceCollection services)
        {
            services.RegisterServicesDependencies();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), ServiceLifetime.Scoped);
            });

            services.AddMediatrValidators();

            return services;
        }

        /// <summary>
        /// Extension method to add request validator in each feature to DI ServiceCollection container.
        /// </summary>
        private static IServiceCollection AddMediatrValidators(this IServiceCollection services)
        {
            // Disable localization for FluentAssertions
            ValidatorOptions.Global.LanguageManager.Enabled = false;

            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly, includeInternalTypes: true);

            return services;
        }
    }
}
