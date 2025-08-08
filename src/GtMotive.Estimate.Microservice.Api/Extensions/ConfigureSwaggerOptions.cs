using System.IdentityModel.Tokens.Jwt;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GtMotive.Estimate.Microservice.Api.Extensions
{
    /// <summary>
    /// Named options class used to configure the order, appearance and the generation of the swagger UI.
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
#pragma warning disable IDE0290
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider"><see cref="IApiVersionDescriptionProvider"/> instance.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }
#pragma warning restore IDE0290
        /// <summary>
        /// Inherited interface method to configure options.
        /// </summary>
        /// <param name="name">Name value.</param>
        /// <param name="options"><see cref="SwaggerGenOptions"/> instance.</param>
        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        /// <summary>
        /// Configures Swagger UI Generation
        /// Sets Title, version, description based on the current api versions, sets the schemas to full name
        /// and includes XML comments.
        /// </summary>
        /// <param name="options"><see cref="SwaggerGenOptions"/> instance.</param>
        public void Configure(SwaggerGenOptions options)
        {
            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
            var deviceApiDescription = "GTMotive";
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = "GTMotive.Test.HectorRomero",
                    Version = description.ApiVersion.ToString(),
                    Description = deviceApiDescription
                });

                options.CustomSchemaIds(type => type.FullName);

                xmlFiles.ForEach(xmlFile => { options.IncludeXmlComments(xmlFile); });
            }

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = JwtConstants.TokenType,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Enumerable.Empty<string>().ToList()
                }
            });
        }
    }
}
