namespace Clinic.WebAPI.EndPoints.Doctors.GetUserInforById.HttpResponseMapper;

internal static class GetUserInforByIdHttpResponseMapper
{
    private static GetUserInforByIdHttpResponseManager _getUserInforByIdHttpResponseManager;

    internal static GetUserInforByIdHttpResponseManager Get()
    {
        return _getUserInforByIdHttpResponseManager ??= new();
    }
}