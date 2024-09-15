using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Firebase.Image;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Firebase;

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
    public static void ConfigFirebaseImageStorage(this IServiceCollection services)
    {
        services.AddSingleton<
            IDefaultUserAvatarAsUrlHandler,
            DefaultUserAvatarAsUrlSourceHandler
        >();
    }
}
