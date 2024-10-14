namespace Clinic.WebAPI.EndPoints.ExaminationServices.HiddenService.HttpResoponseMapper;

/// <summary>
///     Hidden service response manager
/// </summary>
internal static class HiddenServiceHttpResponseMapper
{
    private static HiddenServiceHttpResponseManager _manager;

    internal static HiddenServiceHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
