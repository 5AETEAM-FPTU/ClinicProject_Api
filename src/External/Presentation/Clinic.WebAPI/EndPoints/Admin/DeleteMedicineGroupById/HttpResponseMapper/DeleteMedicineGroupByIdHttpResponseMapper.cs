namespace Clinic.WebAPI.EndPoints.Admin.DeleteMedicineGroupById.HttpResponseMapper;

/// <summary>
///     DeleteMedicineGroupById extension method
/// </summary>
internal static class DeleteMedicineGroupByIdHttpResponseMapper
{
    private static DeleteMedicineGroupByIdHttpResponseManager _deleteMedicineGroupByIdHttpResponseManager;

    internal static DeleteMedicineGroupByIdHttpResponseManager Get()
    {
        return _deleteMedicineGroupByIdHttpResponseManager ??= new();
    }
}
