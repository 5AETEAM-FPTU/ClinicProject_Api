namespace Clinic.Application.Features.Schedules.UpdateSchedule;

/// <summary>
///     CreateSchedules Response Status Code
/// </summary>
public enum UpdateScheduleResponseStatusCode
{
    DATABASE_OPERATION_FAIL,
    SCHEDULE_WAS_OVERLAPED,
    FORBIDEN,
    OPERATION_SUCCESS,
    NOT_FOUND_SCHEDULE,
    SCHEDULE_HAD_APPOINTMENT
}
