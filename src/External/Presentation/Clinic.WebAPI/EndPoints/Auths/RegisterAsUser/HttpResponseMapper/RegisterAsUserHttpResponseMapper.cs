namespace Clinic.WebAPI.EndPoints.Auths.RegisterAsUser.HttpResponseMapper;

/// <summary>
///     RegisterAsUser extension method
/// </summary>
internal static class RegisterAsUserHttpResponseMapper
{
    private static RegisterAsUserHttpResponseManager _RegisterAsUserHttpResponseManager;

    internal static RegisterAsUserHttpResponseManager Get()
    {
        return _RegisterAsUserHttpResponseManager ??= new();
    }
}
