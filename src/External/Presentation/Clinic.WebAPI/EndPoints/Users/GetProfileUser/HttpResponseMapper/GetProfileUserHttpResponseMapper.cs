namespace Clinic.WebAPI.EndPoints.Users.GetProfileUser.HttpResponseMapper;

internal static class GetProfileUserHttpResponseMapper
{
    private static GetProfileUserHttpResponseManager _GetProfileUserHttpResponseManager;

    internal static GetProfileUserHttpResponseManager Get()
    {
        return _GetProfileUserHttpResponseManager ??= new();
    }
}
