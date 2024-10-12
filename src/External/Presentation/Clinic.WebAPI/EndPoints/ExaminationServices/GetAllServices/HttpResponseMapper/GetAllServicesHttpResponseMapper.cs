namespace Clinic.WebAPI.EndPoints.ExaminationServices.GetAllServices.HttpResponseMapper;

/// <summary>
///     GetAllMedicine extension method
/// </summary>
internal static class GetAllServicesHttpResponseMapper
{
    private static GetAllServicesHttpResponseManager _manager;

    internal static GetAllServicesHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
