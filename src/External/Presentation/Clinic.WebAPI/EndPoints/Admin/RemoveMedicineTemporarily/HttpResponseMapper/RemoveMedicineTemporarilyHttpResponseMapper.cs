namespace Clinic.WebAPI.EndPoints.Admin.RemoveMedicineTemporarily.HttpResponseMapper;

/// <summary>
///     RemoveMedicineTemporarily extension method
/// </summary>
internal static class RemoveMedicineTemporarilyHttpResponseMapper
{
    private static RemoveMedicineTemporarilyHttpResponseManager _removeMedicineTemporarilyHttpResponseManager;

    internal static RemoveMedicineTemporarilyHttpResponseManager Get()
    {
        return _removeMedicineTemporarilyHttpResponseManager ??= new();
    }
}
