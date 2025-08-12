using GtMotive.Estimate.Microservice.Api.Extensions;

namespace GtMotive.Estimate.Microservice.Api
{
    /// <summary>
    ///     The entry point and orchestrator of the application's startup process.
    ///     Its significance lies in configuring the web host, setting up services, and initiating the application.
    /// </summary>
    public static partial class Program
    {
        /// <summary>
        ///     This method acts as the starting point of the application.
        /// </summary>
        /// <param name="args">Optional arguments passed to customize the startup process.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="HostBuilder" /> class and configures the provider settings.
        /// </summary>
        /// <param name="args">The command line args.</param>
        /// <returns>The initialized <see cref="IHostBuilder" />.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = HostBuilderExtensions.Create(args);

            hostBuilder.ConfigureUserSecrets();

            hostBuilder.ConfigureAzureAppConfigurationApiSources();

            hostBuilder.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

            return hostBuilder;
        }
    }
}
