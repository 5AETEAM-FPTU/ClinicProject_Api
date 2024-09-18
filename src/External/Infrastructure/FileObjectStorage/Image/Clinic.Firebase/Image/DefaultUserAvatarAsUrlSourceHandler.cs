using Clinic.Application.Commons.FIleObjectStorage;

namespace Clinic.Firebase.Image;

/// <summary>
///     Implementation of default avatar url handler.
/// </summary>
internal sealed class DefaultUserAvatarAsUrlSourceHandler : IDefaultUserAvatarAsUrlHandler
{
    private const string DefaultUserAvatarUrl =
        "https://firebasestorage.googleapis.com/v0/b/clinic-dab90.appspot.com/o/avatardefault_92824%20(1).jpg?alt=media&token=9f3a05d0-b63b-4fbd-9f20-0ed2fa22009f&fbclid=IwY2xjawFXVLRleHRuA2FlbQIxMAABHd48QMtiZUjUXWNb4Pd5zqpVbhjFOIGGZYvaLwcqKxRTGRQIZFN3LFNeBg_aem_rDaIQ1ITYked594N7-pCnw";

    public string Get()
    {
        return DefaultUserAvatarUrl;
    }
}
