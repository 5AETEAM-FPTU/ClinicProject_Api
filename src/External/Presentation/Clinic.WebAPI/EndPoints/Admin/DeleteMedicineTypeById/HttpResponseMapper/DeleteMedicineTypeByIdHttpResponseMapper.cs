namespace Clinic.WebAPI.EndPoints.Admin.DeleteMedicineTypeById.HttpResponseMapper;

/// <summary>
///     DeleteMedicineTypeById extension method
/// </summary>
internal static class DeleteMedicineTypeByIdHttpResponseMapper
{
    private static DeleteMedicineTypeByIdHttpResponseManager _deleteMedicineTypeByIdHttpResponseManager;

    internal static DeleteMedicineTypeByIdHttpResponseManager Get()
    {
        return _deleteMedicineTypeByIdHttpResponseManager ??= new();
    }
}
