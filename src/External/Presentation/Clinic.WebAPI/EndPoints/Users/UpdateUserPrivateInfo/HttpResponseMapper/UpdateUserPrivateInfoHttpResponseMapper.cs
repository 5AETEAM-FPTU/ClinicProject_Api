namespace Clinic.WebAPI.EndPoints.Doctors.UpdateUserPrivateInfo.HttpResponseMapper;

internal static class UpdateUserPrivateInfoHttpResponseMapper
{
    private static UpdateUserPrivateInfoHttpResponseManager _updateUserPrivateInfoHttpResponseManager;

    internal static UpdateUserPrivateInfoHttpResponseManager Get()
    {
        return _updateUserPrivateInfoHttpResponseManager ??= new();
    }
}
