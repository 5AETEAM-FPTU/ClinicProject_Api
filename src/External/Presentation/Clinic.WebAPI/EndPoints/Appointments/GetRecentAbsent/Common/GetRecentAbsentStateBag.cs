using Clinic.Application.Features.Appointments.GetRecentAbsent;

namespace Clinic.WebAPI.EndPoints.Appointments.GetRecentAbsent.Common;

internal sealed class GetRecentAbsentStateBag
{
    internal GetRecentAbsentRequest AppRequest { get; } = new();
}
