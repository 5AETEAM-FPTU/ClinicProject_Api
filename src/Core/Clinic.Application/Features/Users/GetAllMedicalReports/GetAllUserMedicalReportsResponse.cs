using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;

namespace Clinic.Application.Features.Users.GetAllMedicalReports;

/// <summary>
///     GetAllMedicalReport Response
/// </summary>
public class GetAllUserMedicalReportsResponse : IFeatureResponse
{
    public GetAllUserMedicalReportsResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<MedicalReport> MedicalReports { get; init; }

        public sealed class MedicalReport
        {
            public Guid Id { get; init; }
            public string Title { get; init; }
            public DateTime ExaminedDate { get; init; }
            public string Diagnosis { get; init; }
            public Doctor DoctorInfo { get; init; }

            public sealed class Doctor
            {
                public Guid Id { get; init; }
                public string FullName { get; init; }
                public string AvatarUrl { get; init; }
            }
        }
    }
}
