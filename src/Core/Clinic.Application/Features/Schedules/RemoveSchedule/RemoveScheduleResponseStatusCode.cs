namespace Clinic.Application.Features.Schedules.RemoveSchedule;

/// <summary>
///     CreateSchedules Response Status Code
/// </summary>
public enum RemoveScheduleResponseStatusCode
{
    DATABASE_OPERATION_FAIL,
    SCHEDULE_HAD_APPOINTMENT,
    FORBIDEN,
    OPERATION_SUCCESS,
    NOT_FOUND_SCHEDULE
}
