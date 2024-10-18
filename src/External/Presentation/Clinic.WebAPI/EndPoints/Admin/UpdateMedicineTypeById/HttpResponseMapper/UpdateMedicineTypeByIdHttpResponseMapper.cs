namespace Clinic.WebAPI.EndPoints.Admin.UpdateMedicineTypeById.HttpResponseMapper;

/// <summary>
///     UpdateMedicineTypeById extension method
/// </summary>
internal static class UpdateMedicineTypeByIdHttpResponseMapper
{
    private static UpdateMedicineTypeByIdHttpResponseManager _UpdateMedicineTypeByIdHttpResponseManager;

    internal static UpdateMedicineTypeByIdHttpResponseManager Get()
    {
        return _UpdateMedicineTypeByIdHttpResponseManager ??= new();
    }
}