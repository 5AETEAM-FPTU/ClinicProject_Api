namespace Clinic.WebAPI.EndPoints.Appointments.GetAbsentForStaff.HttpResponseMapper;

/// <summary>
///     GetAbsentForStaff extension method
/// </summary>
internal static class GetAbsentForStaffHttpResponseMapper
{
    private static GetAbsentForStaffHttpResponseManager _GetAbsentForStaffHttpResponseManager;

    internal static GetAbsentForStaffHttpResponseManager Get()
    {
        return _GetAbsentForStaffHttpResponseManager ??= new();
    }
}
