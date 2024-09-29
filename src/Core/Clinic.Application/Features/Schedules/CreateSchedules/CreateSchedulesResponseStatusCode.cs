namespace Clinic.Application.Features.Schedules.CreateSchedules;

/// <summary>
///     CreateSchedules Response Status Code
/// </summary>
public enum CreateSchedulesResponseStatusCode
{
    DATABASE_OPERATION_FAIL,
    TIMESLOT_WAS_OVERLAPED,
    FORBIDEN,
    OPERATION_SUCCESS,
    TIMESLOT_IS_EXIST
}
