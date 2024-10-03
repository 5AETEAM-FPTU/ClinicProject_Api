using Clinic.Application.Commons.Abstractions;
using System;


namespace Clinic.Application.Features.Doctors.GetMedicalReportById;

/// <summary>
///     GetMedicalReportById Response Status Code
/// </summary>
public class GetMedicalReportByIdResponse : IFeatureResponse
{
    public GetMedicalReportByIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }
    public sealed class Body
    {
        public PatientInformation PatientInfor { get; init; }

        public sealed class PatientInformation
        {
            public Guid PatientId { get; init; }
            public string FullName { get; init; }
            public DateTime DOB { get; init; }
            public string Avatar { get; init; }
            public string Address { get; init; }
            public string Gender { get; init; }
            public string PhoneNumber { get; init; }

        }
        public ReportDetail MedicalReport { get; init; }
        public sealed class ReportDetail
        {
            public Guid ReportId { get; init; }
            public DateTime Date { get; init; }
            public string MedicalHistory { get; init; }
            public string GeneralCondition { get; init; }
            public string Weight { get; init; }
            public string Height { get; init; }
            public string Pulse { get; init; }
            public string Temperature { get; init; }
            public string BloodPressure { get; init; }
            public string Diagnosis { get; init; }

        }
        public ServiceOrder Service { get; init; }
        public sealed class ServiceOrder
        {
            public Guid ServiceOrderId { get; init; }
            public int Quantity { get; init; }
            public double TotalPrice { get; init; }

        }
        public MedicineOreder Medicine { get; init; }
        public sealed class MedicineOreder 
        { 
            public Guid MedicineOrderId { get; init; }
        }
    }
}    
