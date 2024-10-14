using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Clinic.Configuration.Presentation.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Clinic.WebAPI.Commons.wwwServiceConfigs;

/// <summary>
///     Authentication service config.
/// </summary>
internal static class AuthenticationServiceConfig
{
    internal static void ConfigAuthentication(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        var option = configuration
            .GetRequiredSection(key: "Authentication")
            .Get<JwtAuthenticationOption>();

        TokenValidationParameters tokenValidationParameters =
            new()
            {
                ValidateIssuer = option.Jwt.ValidateIssuer,
                ValidateAudience = option.Jwt.ValidateAudience,
                ValidateLifetime = option.Jwt.ValidateLifetime,
                ValidateIssuerSigningKey = option.Jwt.ValidateIssuerSigningKey,
                RequireExpirationTime = option.Jwt.RequireExpirationTime,
                ValidTypes = option.Jwt.ValidTypes,
                ValidIssuer = option.Jwt.ValidIssuer,
                ValidAudience = option.Jwt.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    key: new HMACSHA256(
                        key: Encoding.UTF8.GetBytes(s: option.Jwt.IssuerSigningKey)
                    ).Key
                )
            };

        services
            .AddSingleton(implementationInstance: option)
            .AddSingleton(implementationInstance: tokenValidationParameters)
            .AddAuthentication(configureOptions: config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions: config =>
            {
                config.TokenValidationParameters = tokenValidationParameters;

                config.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var path = context.HttpContext.Request.Path;
                        if (path.StartsWithSegments("/chat-hub"))
                        {
                            var token = context.Request.Query["token"];
                            if (!string.IsNullOrEmpty(token))
                            {
                                context.Token = token;
                            }
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token is valid");
                        return Task.CompletedTask;
                    }
                };
            });
    }
}
