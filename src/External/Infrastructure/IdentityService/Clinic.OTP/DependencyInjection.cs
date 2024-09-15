using Clinic.Application.Commons.Token.OTP;
using Clinic.OTP.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.OTP;

/// <summary>
///     Config Dependecy Injection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Config App OTP
    /// </summary>
    /// <param name="services">
    ///     Service Collection for managing services.
    /// </param>
    public static void ConfigAppOTP(this IServiceCollection services)
    {
        services.AddSingleton<IOTPHandler, OtpGenerator>();
    }
}
