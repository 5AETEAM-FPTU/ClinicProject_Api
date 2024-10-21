namespace Clinic.Application.Features.Users.GetUserMedicalReport;

/// <summary>
///     GetUserMedicalReport Response Status Code
/// </summary>
public enum GetUserMedicalReportResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_NOT_USER,
    MEDICAL_REPORT_NOT_FOUND
}
