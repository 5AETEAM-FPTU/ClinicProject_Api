namespace Clinic.WebAPI.EndPoints.Users.UpdatePasswordUser.HttpResponseMapper;

/// <summary>
///     UpdatePasswordUser extension method
/// </summary>
internal static class UpdatePasswordUserHttpResponseMapper
{
    private static UpdatePasswordUserHttpResponseManager _UpdatePasswordUserHttpResponseManager;

    internal static UpdatePasswordUserHttpResponseManager Get()
    {
        return _UpdatePasswordUserHttpResponseManager ??= new();
    }
}
