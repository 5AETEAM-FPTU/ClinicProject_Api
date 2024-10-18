namespace Clinic.WebAPI.EndPoints.Admin.GetAllMedicineGroup.HttpResponseMapper;

/// <summary>
///     GetAllMedicineGroup extension method
/// </summary>
internal static class GetAllMedicineGroupHttpResponseMapper
{
    private static GetAllMedicineGroupHttpResponseManager _GetAllMedicineGroupHttpResponseManager;

    internal static GetAllMedicineGroupHttpResponseManager Get()
    {
        return _GetAllMedicineGroupHttpResponseManager ??= new();
    }
}
