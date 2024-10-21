namespace Clinic.Application.Features.Doctors.GetUsersHaveMedicalReport;

/// <summary>
///     GetUsersHaveMedicalReport Response Status Code
/// </summary>
public enum GetUsersHaveMedicalReportResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_NOT_DOCTOR_STAFF,
}
