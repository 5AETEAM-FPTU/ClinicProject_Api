namespace Clinic.WebAPI.EndPoints.ExaminationServices.CreateService.HttpResoponseMapper;

/// <summary>
///     Create service response manager
/// </summary>
internal static class CreateServiceHttpResponseMapper
{
    private static CreateServiceHttpResponseManager _manager;

    internal static CreateServiceHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
