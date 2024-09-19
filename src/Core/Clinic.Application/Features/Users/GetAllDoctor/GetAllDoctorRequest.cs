using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.GetAllDoctor;

/// <summary>
///     GetAllDoctors Request
/// </summary>
public class GetAllDoctorRequest : IFeatureRequest<GetAllDoctorResponse>
{
    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}
