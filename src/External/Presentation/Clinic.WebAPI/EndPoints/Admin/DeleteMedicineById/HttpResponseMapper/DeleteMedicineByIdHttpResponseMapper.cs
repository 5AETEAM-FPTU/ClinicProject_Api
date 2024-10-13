namespace Clinic.WebAPI.EndPoints.Admin.DeleteMedicineById.HttpResponseMapper;

/// <summary>
///     DeleteMedicineById extension method
/// </summary>
internal static class DeleteMedicineByIdHttpResponseMapper
{
    private static DeleteMedicineByIdHttpResponseManager _deleteMedicineByIdHttpResponseManager;

    internal static DeleteMedicineByIdHttpResponseManager Get()
    {
        return _deleteMedicineByIdHttpResponseManager ??= new();
    }
}
