namespace Clinic.WebAPI.EndPoints.Enums.GetAllSpecialty.HttpResponseMapper;

/// <summary>
///     GetAllSpecialty extension method
/// </summary>
internal static class GetAllSpecialtyHttpResponseMapper
{
    private static GetAllSpecialtyHttpResponseManager _GetAllSpecialtyHttpResponseManager;

    internal static GetAllSpecialtyHttpResponseManager Get()
    {
        return _GetAllSpecialtyHttpResponseManager ??= new();
    }
}
