namespace GtMotive.Estimate.Microservice.Api.Extensions
{
    /// <summary>
    /// Adds Swagger generation and its own custom configuration.
    /// </summary>
    public static class SwaggerConfigurationExtensions
    {
        /// <summary>
        /// Adds swagger configuration based on Named Options.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
        /// <returns>Returns services (extension method).</returns>
        public static IServiceCollection AddApiSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();
            return services;
        }
    }
}
