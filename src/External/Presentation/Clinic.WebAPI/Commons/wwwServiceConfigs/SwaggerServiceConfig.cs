using System;
using Clinic.Configuration.Presentation.Swagger;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;

namespace Clinic.WebAPI.Commons.wwwServiceConfigs;

/// <summary>
///     Swagger service config.
/// </summary>
internal static class SwaggerServiceConfig
{
    internal static void ConfigSwagger(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        var option = configuration
            .GetRequiredSection(key: "Swagger")
            .GetRequiredSection(key: "NSwag")
            .Get<NSwagOption>();

        services.SwaggerDocument(documentOption =>
        {
            documentOption.DocumentSettings = setting =>
            {
                setting.PostProcess = document =>
                {
                    document.Info = new()
                    {
                        Version = option.Doc.PostProcess.Info.Version,
                        Title = option.Doc.PostProcess.Info.Title,
                        Description = option.Doc.PostProcess.Info.Description,
                        Contact = new()
                        {
                            Name = option.Doc.PostProcess.Info.Contact.Name,
                            Email = option.Doc.PostProcess.Info.Contact.Email
                        },
                        License = new()
                        {
                            Name = option.Doc.PostProcess.Info.License.Name,
                            Url = new(value: option.Doc.PostProcess.Info.License.Url)
                        }
                    };
                };

                setting.AddAuth(
                    schemeName: JwtBearerDefaults.AuthenticationScheme,
                    securityScheme: new()
                    {
                        Type = (OpenApiSecuritySchemeType)
                            Enum.ToObject(
                                enumType: typeof(OpenApiSecuritySchemeType),
                                value: option.Doc.Auth.Bearer.Type
                            ),
                        In = (OpenApiSecurityApiKeyLocation)
                            Enum.ToObject(
                                enumType: typeof(OpenApiSecurityApiKeyLocation),
                                value: option.Doc.Auth.Bearer.In
                            ),
                        Scheme = option.Doc.Auth.Bearer.Scheme,
                        BearerFormat = option.Doc.Auth.Bearer.BearerFormat,
                        Description = option.Doc.Auth.Bearer.Description,
                    }
                );
            };

            documentOption.EnableJWTBearerAuth = option.EnableJWTBearerAuth;
        });
    }
}
