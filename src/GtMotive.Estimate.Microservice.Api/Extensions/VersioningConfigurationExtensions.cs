using System.Net;
using Asp.Versioning;

namespace GtMotive.Estimate.Microservice.Api.Extensions
{
    /// <summary>
    /// Extension class that configures the versioning of the API.
    /// </summary>
    public static class VersioningConfigurationExtensions
    {
        /// <summary>
        /// Configures the versioning of the API with query string as the parameter to specify the version
        /// It also returns 400 Bad Request when wrong api-version is used on the request
        /// Api grouping is the most common one (major.minor-status) as found in https://github.com/dotnet/aspnet-api-versioning/wiki/API-Explorer-Options#group-name-format.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
        /// <returns>Returns services (extension method).</returns>
        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                    options.UnsupportedApiVersionStatusCode = (int)HttpStatusCode.BadRequest;

                    // Use the query string for versioning
                    options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
                })
                .AddMvc()
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                });
            return services;
        }
    }
}
