using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.GetAllDoctor;

/// <summary>
///     GetAllDoctors Request
/// </summary>
public class GetAllDoctorRequest : IFeatureRequest<GetAllDoctorResponse>
{
    public int PageIndex { get; init; }
    public int PageSize { get; init; }

    public string Keyword { get; init; }
}
