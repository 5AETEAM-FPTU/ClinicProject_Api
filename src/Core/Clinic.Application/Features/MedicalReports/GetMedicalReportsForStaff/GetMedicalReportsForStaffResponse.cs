﻿using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicalReports.GetMedicalReportsForStaff;

/// <summary>
///     GetMedicalReportsForStaff Response
/// </summary>
public class GetMedicalReportsForStaffResponse : IFeatureResponse
{
    public GetMedicalReportsForStaffResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<GroupedReport> GroupedReports { get; init; }

        public sealed class GroupedReport
        {
            public IEnumerable<MedicalReport> MedicalReports { get; init; }
            public DateTime DayOfDate { get; init; }

            public sealed class MedicalReport
            {
                public Guid PatientId { get; init; }
                public Guid ReportId { get; init; }
                public string FullName { get; init; }
                public string Avatar { get; init; }
                public string PhoneNumber { get; init; }
                public string Gender { get; init; }
                public DateTime StartTime { get; init; }
                public DateTime EndTime { get; init; }
                public int Age { get; init; }
                public string Diagnosis { get; init; }

                public Guid ServiceOrderId { get; init; }
                public Guid MedicineOrderId { get; init; }
            }
        }
    }
}