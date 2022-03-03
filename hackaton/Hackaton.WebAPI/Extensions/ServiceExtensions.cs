using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection.Metadata;

namespace Hackaton.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                //var xmlFile = $"{typeof(AssemblyReference).Assembly.GetName().Name}.xml";
                var xmlFile = "Hackaton.WebAPI.Presentation.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
        public class SwaggerDefaultValues : IOperationFilter
        {
            /// <summary>
            /// Applies the filter to the specified operation using the given context...
            /// </summary>
            /// <param name="operation">The operation to apply the filter to.</param>
            /// <param name="context">The current operation filter context.</param>
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var apiDescription = context.ApiDescription;
                operation.Deprecated |= apiDescription.IsDeprecated();
                if (operation.Parameters == null)
                {
                    return;
                }
                foreach (var parameter in operation.Parameters)
                {
                    var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                    var routeInfo = description.RouteInfo;
                    if (string.IsNullOrEmpty(parameter.Name))
                    {
                        parameter.Name = description.ModelMetadata?.Name;
                    }
                    if (parameter.Description == null)
                    {
                        parameter.Description = description.ModelMetadata?.Description;
                    }
                    if (routeInfo == null)
                    {
                        continue;
                    }
                    parameter.Required |= !routeInfo.IsOptional;
                }
            }
        }
        public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
        {
            private readonly IApiVersionDescriptionProvider provider;
            public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
            {
                this.provider = provider;
            }
            public void Configure(SwaggerGenOptions options)
            {
                // add a swagger document for each discovered API version
                // note: you might choose to skip or document deprecated API versions differently
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                }
            }
            private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
            {
                var info = new OpenApiInfo()
                {
                    Title = "Life API",
                    Version = description.ApiVersion.ToString(),
                    Description = "An API to help Syone manage its resources",
                    Contact = new OpenApiContact()
                    {
                        Name = "Syone",
                        Email = "helpdesk@syone.com; pedro.nova@syone.com; pedro.santos@syone.com; raul.ribeiro@syone.com",
                        Url = new Uri("https://www.syone.com/")
                    },
                    TermsOfService = new Uri("https://www.syone.com/terms-and-conditions-privacy-policy")
                };
                if (description.IsDeprecated)
                {
                    info.Description += " This API version has been deprecated.";
                }
                return info;
            }
        }
    }
}