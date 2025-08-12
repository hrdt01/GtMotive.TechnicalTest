using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GtMotive.Estimate.Microservice.Api.UnitTest.Helpers
{
    /// <inheritdoc />
    public class WebApplicationFixture : WebApplicationFactory<Startup>
    {
        /// <inheritdoc />
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.UseEnvironment("IntegrationTests");
        }
    }
}
