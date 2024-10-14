namespace Clinic.WebAPI.EndPoints.Admin.UpdateMedicineGroupById.HttpResponseMapper;

/// <summary>
///     UpdateMedicineGroupById extension method
/// </summary>
internal static class UpdateMedicineGroupByIdHttpResponseMapper
{
    private static UpdateMedicineGroupByIdHttpResponseManager _UpdateMedicineGroupByIdHttpResponseManager;

    internal static UpdateMedicineGroupByIdHttpResponseManager Get()
    {
        return _UpdateMedicineGroupByIdHttpResponseManager ??= new();
    }
}
