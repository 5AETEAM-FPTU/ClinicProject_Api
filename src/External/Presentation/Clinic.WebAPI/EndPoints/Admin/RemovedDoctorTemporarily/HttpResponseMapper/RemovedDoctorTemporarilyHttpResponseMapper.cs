namespace Clinic.WebAPI.EndPoints.Admin.RemovedDoctorTemporarily.HttpResponseMapper;

/// <summary>
///     RemovedDoctorTemporarily extension method
/// </summary>
internal static class RemovedDoctorTemporarilyHttpResponseMapper
{
    private static RemovedDoctorTemporarilyHttpResponseManager _removeMedicineTemporarilyHttpResponseManager;

    internal static RemovedDoctorTemporarilyHttpResponseManager Get()
    {
        return _removeMedicineTemporarilyHttpResponseManager ??= new();
    }
}
