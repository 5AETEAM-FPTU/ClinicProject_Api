namespace Clinic.WebAPI.EndPoints.Enums.GetAllGender.HttpResponseMapper;

/// <summary>
///     GetAllGender extension method
/// </summary>
internal static class GetAllGenderHttpResponseMapper
{
    private static GetAllGenderHttpResponseManager _GetAllGenderHttpResponseManager;

    internal static GetAllGenderHttpResponseManager Get()
    {
        return _GetAllGenderHttpResponseManager ??= new();
    }
}

