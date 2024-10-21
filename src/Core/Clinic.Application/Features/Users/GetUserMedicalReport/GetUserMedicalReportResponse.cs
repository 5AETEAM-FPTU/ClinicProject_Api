using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.GetUserMedicalReport;

/// <summary>
///     GetUserMedicalReport Response
/// </summary>
public class GetUserMedicalReportResponse : IFeatureResponse
{
    public GetUserMedicalReportResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public Doctor DoctorInfor { get; init; }
        public Patient PatientInfor { get; init; }
        public ReportDetail Detail { get; init; }

        public sealed class Doctor
        {
            public Guid DoctorId { get; init; }
            public string DoctorName { get; init; }
            public string DoctorAvatar { get; init; }
            public IEnumerable<Specialty> DoctorSpecialties { get; init; }
            public Position DoctorPosition { get; init; }

            public sealed class Specialty
            {
                public Guid SpecialtyId { get; init; }
                public string SpecialtyName { get; init; }
                public string SpecialtyConstant { get; init; }
            }
            public sealed class Position
            {
                public Guid PositionId { get; init; }
                public string PositionName { get; init; }
                public string PositionConstant { get; init; }
            }

        }

        public sealed class Patient
        {
            public Guid PatientId { get; init; }
            public string PatientName { get; init; }
            public DateTime DOB { get; init; }
            public string Address { get; init; }
            public string PhoneNumber { get; init; }
            public string PatientAvatar { get; init; }
            public string PatientGender { get; init; }

        }

        public sealed class ReportDetail
        {
            public Guid ReportId { get; init; }
            public DateTime Date { get; init; }
            public string MedicalHistory { get; init; }
            public string GeneralCondition { get; init; }
            public string Height { get; init; }
            public string Weight { get; init; }
            public string Pulse { get; init; }
            public string Temperature { get; init; }
            public string BloodPressure { get; init; }
            public string Diagnosis { get; init; }
            public Guid ServiceOrderId { get; init; }
            public Guid MedicineOrderId { get; init; }
            public bool HasFeedback { get; init; }
        }
    }
}
