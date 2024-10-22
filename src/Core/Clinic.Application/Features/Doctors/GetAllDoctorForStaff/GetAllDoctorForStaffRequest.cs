using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Doctors.GetAllDoctorForStaff;

/// <summary>
///     GetAllDoctorForStaff Request
/// </summary>
public class GetAllDoctorForStaffRequest : IFeatureRequest<GetAllDoctorForStaffResponse>
{
    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 6;

    [BindFrom("keyWord")]
    public string KeyWord { get; init; }
}
