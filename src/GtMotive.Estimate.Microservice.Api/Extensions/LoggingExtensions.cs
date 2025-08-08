using Azure.Monitor.OpenTelemetry.AspNetCore;
using Azure.Monitor.OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace GtMotive.Estimate.Microservice.Api.Extensions
{
    /// <summary>
    /// LoggingExtensions definition.
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// Add to a service collection the resources necessary to configure metrics, traces and logs to Application Insights.
        /// Only if the Application Insights is defined.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="configuration">Configuration instance.</param>
        /// <param name="appName">Appliaction name.</param>
        /// <returns>Updated current contract.</returns>
        public static IServiceCollection ConfigureOpenTelemetry(this IServiceCollection services, IConfiguration configuration, string appName)
        {
            ArgumentNullException.ThrowIfNull(services);
            var connectionsString = configuration.GetValue<string>("APPLICATIONINSIGHTS_CONNECTION_STRING");

            if (!string.IsNullOrEmpty(connectionsString))
            {
                services.AddOpenTelemetry()
                    .UseAzureMonitor(options =>
                    {
                        options.ConnectionString = connectionsString;
                    })
                    .WithMetrics(metrics =>
                    {
                        metrics
                            .AddAspNetCoreInstrumentation()
                            .AddHttpClientInstrumentation()
                            .AddAzureMonitorMetricExporter(options =>
                            {
                                options.ConnectionString = connectionsString;
                            })
                            .ConfigureResource(r => r.AddService(appName));
                    })
                    .WithTracing(tracing =>
                    {
                        tracing
                            .AddAspNetCoreInstrumentation()
                            .AddHttpClientInstrumentation()
                            .AddAzureMonitorTraceExporter(options =>
                            {
                                options.ConnectionString = connectionsString;
                            })
                            .ConfigureResource(r => r.AddService(appName));
                    })
                    .WithLogging(
                        configureBuilder =>
                        {
                            configureBuilder.ConfigureResource(r => r.AddService(appName));
                        },
                        configureOptions =>
                        {
                            configureOptions.IncludeFormattedMessage = true;
                            configureOptions.IncludeScopes = true;
                            configureOptions.AddAzureMonitorLogExporter(options =>
                            {
                                options.ConnectionString = connectionsString;
                            });
                        });
            }

            return services;
        }
    }
}
