using Clinic.Application.Commons.Payment;
using Clinic.Configuration.Infrastructure.Payment.VNPay;
using Clinic.VNPAY.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.VNPAY;

/// <summary>
///     Configure services for smtp layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Configuration manager.
    /// </param>
    public static void ConfigVNPay(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.AddSingleton(configuration.GetRequiredSection(key: "VNPay").Get<VNPayOption>());

        services.AddSingleton<IPaymentHandler, VNPayHandler>();
    }
}
