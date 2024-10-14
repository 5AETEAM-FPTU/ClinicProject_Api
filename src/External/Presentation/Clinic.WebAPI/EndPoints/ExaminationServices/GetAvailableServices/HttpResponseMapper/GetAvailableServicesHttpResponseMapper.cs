namespace Clinic.WebAPI.EndPoints.ExaminationServices.GetAvailableServices.HttpResponseMapper;

/// <summary>
///     GetAvailableSerivces extension method
/// </summary>
internal static class GetAvailableServicesHttpResponseMapper
{
    private static GetAvailableServicesHttpResponseManager _manager;

    internal static GetAvailableServicesHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
