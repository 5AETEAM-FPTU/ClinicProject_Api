using Clinic.Application.Commons.Token.AccessToken;
using Clinic.Application.Commons.Token.RefreshToken;
using Clinic.JsonWebToken.Handler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.JsonWebToken;

/// <summary>
///     Configure services for this layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    public static void ConfigureJwtIdentityService(this IServiceCollection services)
    {
        services.AddSingleton<JsonWebTokenHandler>();

        services
            .AddSingleton<IAccessTokenHandler, AccessTokenHandler>()
            .AddSingleton<IRefreshTokenHandler, RefreshTokenHandler>();
    }
}
