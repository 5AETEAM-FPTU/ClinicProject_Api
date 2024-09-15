using System;
using System.Threading.RateLimiting;
using Clinic.Configuration.Presentation.RateLimiter;
using Clinic.WebAPI.Commons.AppCodes;
using Clinic.WebAPI.Commons.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.WebAPI.Commons.wwwServiceConfigs;

/// <summary>
///     RateLimited service config.
/// </summary>
internal static class RateLimitedServiceConfig
{
    /// <summary>
    ///     Configure the rate limiter service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    internal static void ConfigRateLimiter(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.AddRateLimiter(configureOptions: config =>
        {
            config.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
                partitioner: context =>
                {
                    var option = configuration
                        .GetRequiredSection(key: "ApiRateLimiter")
                        .GetRequiredSection(key: "FixedWindow")
                        .Get<FixedWindowRateLimiterOption>();

                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: context.Connection.RemoteIpAddress.ToString(),
                        factory: _ =>
                            new()
                            {
                                PermitLimit = option.RemoteIpAddress.PermitLimit,
                                QueueProcessingOrder = (QueueProcessingOrder)
                                    Enum.ToObject(
                                        enumType: typeof(QueueProcessingOrder),
                                        value: option.RemoteIpAddress.QueueProcessingOrder
                                    ),
                                QueueLimit = option.RemoteIpAddress.QueueLimit,
                                Window = TimeSpan.FromSeconds(value: option.RemoteIpAddress.Window),
                                AutoReplenishment = option.RemoteIpAddress.AutoReplenishment,
                            }
                    );
                }
            );

            config.OnRejected = async (option, cancellationToken) =>
            {
                option.HttpContext.Response.Clear();
                option.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                await option.HttpContext.Response.WriteAsJsonAsync(
                    value: new CommonApiResponse
                    {
                        AppCode = CommonAppCode.SERVER_ERROR.ToString(),
                        ErrorMessages = ["Two many request.", "Please try again later."]
                    },
                    cancellationToken: cancellationToken
                );

                await option.HttpContext.Response.CompleteAsync();
            };
        });
    }
}
