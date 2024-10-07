using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.GetAllMedicalReport;

/// <summary>
///     GetAllMedicalReport Request
/// </summary>
public class GetAllMedicalReportRequest : IFeatureRequest<GetAllMedicalReportResponse>
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }
}
