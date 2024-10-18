namespace Clinic.WebAPI.EndPoints.ExaminationServices.UpdateService.HttpResoponseMapper;

/// <summary>
///     Create service response manager
/// </summary>
internal static class UpdateServiceHttpResponseMapper
{
    private static UpdateServiceHttpResponseManager _manager;

    internal static UpdateServiceHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
