namespace Clinic.Application.Features.MedicalReports.CreateMedicalReport;

/// <summary>
///     CreateMedicalReportResponse Status Code
/// </summary>
public enum CreateMedicalReportResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    FORBIDEN_ACCESS,
    APPOINTMENT_IS_NOT_FOUND,
    APPOINTMENT_HAS_ALREADY_REPORT,
    PATIENT_IS_NOT_FOUND,
}
