namespace Clinic.WebAPI.EndPoints.Admin.GetAllMedicineType.HttpResponseMapper;

/// <summary>
///     GetAllMedicineType extension method
/// </summary>
internal static class GetAllMedicineTypeHttpResponseMapper
{
    private static GetAllMedicineTypeHttpResponseManager _GetAllMedicineTypeHttpResponseManager;

    internal static GetAllMedicineTypeHttpResponseManager Get()
    {
        return _GetAllMedicineTypeHttpResponseManager ??= new();
    }
}
