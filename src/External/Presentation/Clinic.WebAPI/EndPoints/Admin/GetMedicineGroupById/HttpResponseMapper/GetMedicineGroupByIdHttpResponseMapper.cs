namespace Clinic.WebAPI.EndPoints.Admin.GetMedicineGroupById.HttpResponseMapper;

/// <summary>
///     GetMedicineGroupById extension method
/// </summary>
internal static class GetMedicineGroupByIdHttpResponseMapper
{
    private static GetMedicineGroupByIdHttpResponseManager _GetMedicineGroupByIdHttpResponseManager;

    internal static GetMedicineGroupByIdHttpResponseManager Get()
    {
        return _GetMedicineGroupByIdHttpResponseManager ??= new();
    }
}

