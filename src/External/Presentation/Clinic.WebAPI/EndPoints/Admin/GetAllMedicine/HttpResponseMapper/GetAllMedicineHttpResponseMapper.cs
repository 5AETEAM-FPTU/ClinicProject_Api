namespace Clinic.WebAPI.EndPoints.Admin.GetAllMedicine.HttpResponseMapper;

/// <summary>
///     GetAllMedicine extension method
/// </summary>
internal static class GetAllMedicineHttpResponseMapper
{
    private static GetAllMedicineHttpResponseManager _GetAllMedicineHttpResponseManager;

    internal static GetAllMedicineHttpResponseManager Get()
    {
        return _GetAllMedicineHttpResponseManager ??= new();
    }
}
