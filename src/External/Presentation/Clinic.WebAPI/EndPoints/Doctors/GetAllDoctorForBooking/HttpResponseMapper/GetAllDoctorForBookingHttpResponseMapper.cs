namespace Clinic.WebAPI.EndPoints.Doctors.GetAllDoctorForBooking.HttpResponseMapper;

/// <summary>
///     GetAllDoctorForBooking extension method
/// </summary>
internal static class GetAllDoctorForBookingHttpResponseMapper
{
    private static GetAllDoctorForBookingHttpResponseManager _GetAllDoctorForBookingHttpResponseManager;

    internal static GetAllDoctorForBookingHttpResponseManager Get()
    {
        return _GetAllDoctorForBookingHttpResponseManager ??= new();
    }
}
