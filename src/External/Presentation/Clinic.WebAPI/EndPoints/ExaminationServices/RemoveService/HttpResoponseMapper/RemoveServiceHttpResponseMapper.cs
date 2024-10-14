namespace Clinic.WebAPI.EndPoints.ExaminationServices.RemoveService.HttpResoponseMapper;

/// <summary>
///     Remove service response manager
/// </summary>
internal static class RemoveServiceHttpResponseMapper
{
    private static RemoveServiceHttpResponseManager _manager;

    internal static RemoveServiceHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
