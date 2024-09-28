namespace Clinic.WebAPI.EndPoints.Enums.GetAllRetreatmentType.HttpResponseMapper;

/// <summary>
///     GetAllRetreatmentType extension method
/// </summary>
internal static class GetAllRetreatmentTypeHttpResponseMapper
{
    private static GetAllRetreatmentTypeHttpResponseManager _GetAllRetreatmentTypeHttpResponseManager;

    internal static GetAllRetreatmentTypeHttpResponseManager Get()
    {
        return _GetAllRetreatmentTypeHttpResponseManager ??= new();
    }
}
