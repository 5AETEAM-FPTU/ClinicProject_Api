using Clinic.Application.Commons.Abstractions;
using System.Collections.Generic;
using System;

namespace Clinic.Application.Features.Users.GetRecentMedicalReport;

/// <summary>
///     GetRecentMedicalReport Response
/// </summary>
public class GetRecentMedicalReportResponse : IFeatureResponse
{
    public GetRecentMedicalReportResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<MedicalReport> MedicalReports { get; init; }
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

