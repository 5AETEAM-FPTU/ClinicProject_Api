namespace Clinic.WebAPI.EndPoints.Admin.CreateNewMedicineType.HttpResponseMapper;

/// <summary>
///     CreateNewMedicineType extension method
/// </summary>
internal static class CreateNewMedicineTypeHttpResponseMapper
{
    private static CreateNewMedicineTypeHttpResponseManager _CreateNewMedicineTypeHttpResponseManager;

    internal static CreateNewMedicineTypeHttpResponseManager Get()
    {
        return _CreateNewMedicineTypeHttpResponseManager ??= new();
    }
}
