using Clinic.Application.Features.Auths.Logout;

namespace Clinic.WebAPI.EndPoints.Auths.Logout.Common;

/// <summary>
///     Represents the state bag used for the Logout flow.
/// </summary>
internal sealed class LogoutStateBag
{
    internal LogoutRequest AppRequest { get; } = new();
}
