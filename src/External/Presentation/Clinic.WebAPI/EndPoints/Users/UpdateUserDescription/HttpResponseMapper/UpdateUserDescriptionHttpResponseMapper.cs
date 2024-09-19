namespace Clinic.WebAPI.EndPoints.Doctors.UpdateUserDescription.HttpResponseMapper;

internal static class UpdateUserDescriptionHttpResponseMapper
{
    private static UpdateUserDescriptionHttpResponseManager _updateUserDescriptionHttpResponseManager;

    internal static UpdateUserDescriptionHttpResponseManager Get()
    {
        return _updateUserDescriptionHttpResponseManager ??= new();
    }
}
