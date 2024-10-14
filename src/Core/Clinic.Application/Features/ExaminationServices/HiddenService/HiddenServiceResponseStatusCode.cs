namespace Clinic.Application.Features.ExaminationServices.HiddenService;

/// <summary>
///     HiddenService Response Status Code
/// </summary>
public enum HiddenServiceResponseStatusCode
{
    DATABASE_OPERATION_FAIL,
    SCHEDULE_HAD_APPOINTMENT,
    FORBIDEN,
    OPERATION_SUCCESS,
    SERVICE_NOT_FOUND
}
