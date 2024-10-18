namespace Clinic.WebAPI.EndPoints.ExaminationServices.GetDetailService.HttpResponseMapper;

internal static class GetDetailServiceHttpResponseMapper
{
    private static GetDetailServiceHttpResponseManager _manager;

    internal static GetDetailServiceHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
