using System;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;

namespace Clinic.Application.Features.Doctors.GetRecentMedicalReportByUserId;

/// <summary>
///     GetRecentMedicalReportByUserId Response
/// </summary>
public class GetRecentMedicalReportByUserIdResponse : IFeatureResponse
{
    public GetRecentMedicalReportByUserIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<MedicalReport> MedicalReports { get; init; }

        public sealed class MedicalReport
        {
            public Guid DoctorId { get; init; }
            public Guid ReportId { get; init; }
            public string FullName { get; init; }
            public string Avatar { get; init; }
            public DateTime StartTime { get; init; }
            public DateTime EndTime { get; init; }
            public DateTime Date { get; init; }
            public string Diagnosis { get; init; }
            public string Title { get; init; }
        }
    }
}
