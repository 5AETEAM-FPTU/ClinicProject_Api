using Clinic.WebAPI.EndPoints.Doctors.UpdateDoctorDescription.HttpResponseMapper;

namespace Clinic.WebAPI.EndPoints.Doctors.UpdateDutyStatus.HttpResponseMapper;

internal class UpdateDutyStatusHttpResponseMapper
{
    private static UpdateDutyStatusHttpResponseManager updateDutyStatusHttpResponseManager;

    internal static UpdateDutyStatusHttpResponseManager Get()
    {
        return updateDutyStatusHttpResponseManager ??= new();
    }
}
