using System.Reflection;
using Clinic.Configuration.Infrastructure.Database;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.MySQL.wwwServiceConfigs;

/// <summary>
///     MySqlDbContextPool service config.
/// </summary>
internal static class MySqlDbContextPoolServiceConfig
{
    internal static void ConfigMySqlDbContextPool(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.AddDbContextPool<ClinicContext>(
            optionsAction: (provider, config) =>
            {
                var option = configuration
                    .GetRequiredSection(key: "Database")
                    .GetRequiredSection(key: "Clinic")
                    .Get<DatabaseOption>();

                config
                    .UseMySQL(
                        connectionString: option.ConnectionString,
                        mySqlOptionsAction: mySqlOptionsAction =>
                        {
                            mySqlOptionsAction
                                .CommandTimeout(commandTimeout: option.CommandTimeOut)
                                .EnableRetryOnFailure(maxRetryCount: option.EnableRetryOnFailure)
                                .MigrationsAssembly(
                                    assemblyName: Assembly.GetExecutingAssembly().GetName().Name
                                );
                        }
                    )
                    .EnableSensitiveDataLogging(
                        sensitiveDataLoggingEnabled: option.EnableSensitiveDataLogging
                    )
                    .EnableDetailedErrors(detailedErrorsEnabled: option.EnableDetailedErrors)
                    .EnableThreadSafetyChecks(enableChecks: option.EnableThreadSafetyChecks)
                    .EnableServiceProviderCaching(
                        cacheServiceProvider: option.EnableServiceProviderCaching
                    );
            }
        );
        services.AddDbContextFactory<ClinicContext>(
            optionsAction: (provider, config) =>
            {
                var option = configuration
                    .GetRequiredSection(key: "Database")
                    .GetRequiredSection(key: "Clinic")
                    .Get<DatabaseOption>();

                config
                    .UseMySQL(
                        connectionString: option.ConnectionString,
                        mySqlOptionsAction: mySqlOptionsAction =>
                        {
                            mySqlOptionsAction
                                .CommandTimeout(commandTimeout: option.CommandTimeOut)
                                .EnableRetryOnFailure(maxRetryCount: option.EnableRetryOnFailure)
                                .MigrationsAssembly(
                                    assemblyName: Assembly.GetExecutingAssembly().GetName().Name
                                );
                        }
                    )
                    .EnableSensitiveDataLogging(
                        sensitiveDataLoggingEnabled: option.EnableSensitiveDataLogging
                    )
                    .EnableDetailedErrors(detailedErrorsEnabled: option.EnableDetailedErrors)
                    .EnableThreadSafetyChecks(enableChecks: option.EnableThreadSafetyChecks)
                    .EnableServiceProviderCaching(
                        cacheServiceProvider: option.EnableServiceProviderCaching
                    );
            },
            ServiceLifetime.Scoped
        );
    }
}
