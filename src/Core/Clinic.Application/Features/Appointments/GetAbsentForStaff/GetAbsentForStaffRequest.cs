using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Appointments.GetAbsentForStaff;

/// <summary>
///     GetAbsentForStaff Request
/// </summary>
public class GetAbsentForStaffRequest : IFeatureRequest<GetAbsentForStaffResponse>
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }
}
