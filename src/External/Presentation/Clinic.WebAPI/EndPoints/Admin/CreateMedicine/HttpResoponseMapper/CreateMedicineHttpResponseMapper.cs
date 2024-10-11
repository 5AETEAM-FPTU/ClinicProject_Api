namespace Clinic.WebAPI.EndPoints.Admin.CreateMedicine.HttpResoponseMapper;

/// <summary>
///     CreateMedicine extension method
/// </summary>
internal static class CreateMedicineHttpResponseMapper
{
    private static CreateMedicineHttpResponseManager _CreateMedicineHttpResponseManager;

    internal static CreateMedicineHttpResponseManager Get()
    {
        return _CreateMedicineHttpResponseManager ??= new();
    }
}
