using System.Text.Json;
using System.Text.Json.Serialization;
using Asp.Versioning.ApiExplorer;
using GtMotive.Estimate.Microservice.Api.Extensions;
using GtMotive.Estimate.Microservice.Application.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace GtMotive.Estimate.Microservice.Api
{
    /// <summary>
    /// Program start up class that contains the initial methods for run the application.
    /// </summary>
    public class Startup
    {
#pragma warning disable IDE0290
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration"><see cref="IConfiguration"/> instance.</param>
        /// <param name="environment"><see cref="IWebHostEnvironment"/> instance.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
#pragma warning restore IDE0290

        /// <summary>
        /// Gets or sets a set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; protected set; }

        /// <summary>
        /// Gets or sets information about the web hosting environment an application is running in.
        /// </summary>
        public IWebHostEnvironment Environment { get; protected set; }

        /// <summary>
        /// Configure ASP.Net Core Middlewares.
        /// </summary>
        /// <param name="app">Defines a class that provides the mechanisms to configure an application's request pipeline.</param>
        public static void Configure(IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            var isSwaggerEnabled = true;
            if (isSwaggerEnabled)
            {
                var apiService = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    var descriptions = apiService.ApiVersionDescriptions;
                    foreach (var groupName in descriptions.Select(item => item.GroupName))
                    {
                        options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
                    }
                });
            }

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true
            });

            app.UseExceptionHandler();
            app.UseStatusCodePages();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Inject services ASP.Net Core DI.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(option =>
                {
                    option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    option.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                    option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddEndpointsApiExplorer();
            services.AddCustomApiVersioning();
            services.AddApiSwagger();

            services.AddProblemDetails();

            services.AddHealthChecks();

            services.AddApplicationArtifacts();

            services.ConfigureOpenTelemetry(Configuration, Environment.ApplicationName);
        }
    }
}
