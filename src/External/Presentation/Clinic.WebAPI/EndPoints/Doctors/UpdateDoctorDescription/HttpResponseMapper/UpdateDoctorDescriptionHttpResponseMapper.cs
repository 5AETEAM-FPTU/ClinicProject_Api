namespace Clinic.WebAPI.EndPoints.Doctors.UpdateDoctorDescription.HttpResponseMapper;

internal static class UpdateDoctorDescriptionHttpResponseMapper
{
    private static UpdateDoctorDescriptionHttpResponseManager updateDoctorDescriptionHttpResponseManager;

    internal static UpdateDoctorDescriptionHttpResponseManager Get()
    {
        return updateDoctorDescriptionHttpResponseManager ??= new();
    }
}
