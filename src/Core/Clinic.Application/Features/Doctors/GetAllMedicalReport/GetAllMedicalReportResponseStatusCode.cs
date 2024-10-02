namespace Clinic.Application.Features.Doctors.GetAllMedicalReport;

/// <summary>
///     GetAllMedicalReport Response Status Code
/// </summary>
public enum GetAllMedicalReportResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_NOT_DOCTOR_STAFF
}
