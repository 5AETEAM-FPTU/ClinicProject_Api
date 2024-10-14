namespace Clinic.Application.Features.ExaminationServices.RemoveService;

/// <summary>
///     RemoveService Response Status Code
/// </summary>
public enum RemoveServiceResponseStatusCode
{
    DATABASE_OPERATION_FAIL,
    SCHEDULE_HAD_APPOINTMENT,
    FORBIDEN,
    OPERATION_SUCCESS,
    SERVICE_NOT_FOUND
}
