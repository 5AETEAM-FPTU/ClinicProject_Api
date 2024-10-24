namespace Clinic.WebAPI.EndPoints.Feedbacks.DoctorGetAllFeedbacks.HttpResponseMapper;

/// <summary>
///     DoctorGetAllFeedback extension method
/// </summary>
internal static class DoctorGetAllFeedBackHttpResponseMapper
{
    private static DoctorGetAllFeedBackHttpResponseManager _manager;

    internal static DoctorGetAllFeedBackHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
