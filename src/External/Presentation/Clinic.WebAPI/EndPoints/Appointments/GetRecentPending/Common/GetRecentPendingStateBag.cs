using Clinic.Application.Features.Appointments.GetRecentPending;

namespace Clinic.WebAPI.EndPoints.Appointments.GetRecentPending.Common;

internal sealed class GetRecentPendingStateBag
{
    internal GetRecentPendingRequest AppRequest { get; } = new();
}
