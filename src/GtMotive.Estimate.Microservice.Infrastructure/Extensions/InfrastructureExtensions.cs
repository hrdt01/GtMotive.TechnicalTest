using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Database;
using GtMotive.Estimate.Microservice.Infrastructure.Implementation;
using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions register.
    /// </summary>
    public static class InfrastructureExtensions
    {
        /// <summary>
        /// Add to the DI Service Collection the services needed to work with injected services.
        /// </summary>
        /// <param name="services">Current contract where the services will be added.</param>
        /// <returns>Updated current contract.</returns>
        public static IServiceCollection RegisterInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddDbContext<FleetContext>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IFleetRepository, FleetRepository>();

            services.AddSingleton<ICustomerEntityFactory, EntityFactory>();
            services.AddSingleton<IVehicleEntityFactory, EntityFactory>();
            services.AddSingleton<IRentedVehicleEntityFactory, EntityFactory>();
            services.AddSingleton<IFleetEntityFactory, EntityFactory>();

            return services;
        }
    }
}
