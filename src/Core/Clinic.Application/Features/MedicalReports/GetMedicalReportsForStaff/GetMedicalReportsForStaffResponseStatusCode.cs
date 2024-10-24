namespace Clinic.Application.Features.MedicalReports.GetMedicalReportsForStaff;

/// <summary>
///     GetMedicalReportsForStaff Response Status Code
/// </summary>
public enum GetMedicalReportsForStaffResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_NOT_DOCTOR_STAFF
}
