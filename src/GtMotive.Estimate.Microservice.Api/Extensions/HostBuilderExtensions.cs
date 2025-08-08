using System.Reflection;

namespace GtMotive.Estimate.Microservice.Api.Extensions
{
    /// <summary>
    /// HostBuilderExtensions definition.
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostBuilder"/> class with pre-configured defaults.
        /// </summary>
        /// <remarks>
        ///   The following defaults are applied to the returned <see cref="HostBuilder"/>:
        ///   <list type="bullet">
        ///     <item><description>set the <see cref="IHostEnvironment.ContentRootPath"/> to the result of <see cref="Directory.GetCurrentDirectory()"/></description></item>
        ///     <item><description>load host <see cref="IConfiguration"/> from "DOTNET_" prefixed environment variables</description></item>
        ///     <item><description>load host <see cref="IConfiguration"/> from supplied command line args</description></item>
        ///     <item><description>enables scope validation on the dependency injection container when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development'</description></item>
        ///   </list>
        /// </remarks>
        /// <param name="args">The command line args.</param>
        /// <returns>The initialized <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder Create(params string[] args)
        {
            return new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureHostConfiguration(config =>
                {
                    config.AddEnvironmentVariables(prefix: "DOTNET_");
                    if (args is { Length: > 0 })
                    {
                        config.AddCommandLine(args);
                    }
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    var isDevelopment = context.HostingEnvironment.IsDevelopment();
                    options.ValidateScopes = isDevelopment;
                    options.ValidateOnBuild = isDevelopment;
                });
        }

        /// <summary>
        /// Configures an existing <see cref="IHostBuilder"/> instance with the user secrets for local purposes.
        /// </summary>
        /// <remarks>
        ///   The following defaults are applied to the <see cref="IHostBuilder"/>:
        ///     * load app <see cref="IConfiguration"/> from User Secrets when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development' using the entry assembly
        ///     * load app <see cref="IConfiguration"/> from environment variables.
        /// </remarks>
        /// <param name="builder">The existing builder to configure.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder ConfigureUserSecrets(this IHostBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            return builder.ConfigureAppConfiguration((hostContext, config) =>
            {
                var env = hostContext.HostingEnvironment;
                if (env.IsDevelopment() && env.ApplicationName is { Length: > 0 })
                {
                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    if (appAssembly is not null)
                    {
                        config.AddUserSecrets(appAssembly, optional: true, reloadOnChange: true);
                    }
                }

                config.AddEnvironmentVariables();
            });
        }

        /// <summary>
        /// Configures an existing <see cref="IHostBuilder"/> instance with the settings files .
        /// </summary>
        /// <param name="builder">The existing builder to configure.</param>
        public static void ConfigureAzureAppConfigurationApiSources(this IHostBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            builder.ConfigureAppConfiguration((hostContext, config) =>
            {
                var envName = hostContext.HostingEnvironment.EnvironmentName;
                config.AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            });
        }
    }
}
