namespace Clinic.WebAPI.EndPoints.Admin.UpdateMedicine.HttpResponseMapper;

/// <summary>
///     UpdateMedicine extension method
/// </summary>
internal static class UpdateMedicineHttpResponseMapper
{
    private static UpdateMedicineHttpResponseManager _UpdateMedicineHttpResponseManager;

    internal static UpdateMedicineHttpResponseManager Get()
    {
        return _UpdateMedicineHttpResponseManager ??= new();
    }
}
