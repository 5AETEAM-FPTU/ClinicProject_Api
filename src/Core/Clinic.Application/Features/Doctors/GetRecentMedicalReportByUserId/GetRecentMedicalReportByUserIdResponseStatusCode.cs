namespace Clinic.Application.Features.Doctors.GetRecentMedicalReportByUserId;

/// <summary>
///     GetRecentMedicalReportByUserId Response Status Code
/// </summary>
public enum GetRecentMedicalReportByUserIdResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_NOT_DOCTOR_STAFF,
    USER_ID_NOT_FOUND
}
