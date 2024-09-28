namespace Clinic.WebAPI.EndPoints.Enums.GetAllPosition.HttpResponseMapper;

/// <summary>
///     GetAllPosition extension method
/// </summary>
internal static class GetAllPositionHttpResponseMapper
{
    private static GetAllPositionHttpResponseManager _GetAllPositionHttpResponseManager;

    internal static GetAllPositionHttpResponseManager Get()
    {
        return _GetAllPositionHttpResponseManager ??= new();
    }
}

