namespace Clinic.WebAPI.EndPoints.Users.GetAllUser.HttpResponseMapper;
/// <summary>
///     GetAllDoctors extension method
/// </summary>
internal static class GetAllUserHttpResponseMapper
{
    private static GetAllUserHttpResponseManager _GetAllUserHttpResponseManager;

    internal static GetAllUserHttpResponseManager Get()
    {
        return _GetAllUserHttpResponseManager ??= new();
    }
}

