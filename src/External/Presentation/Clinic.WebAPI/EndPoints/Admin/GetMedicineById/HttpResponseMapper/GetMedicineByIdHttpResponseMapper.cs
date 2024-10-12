namespace Clinic.WebAPI.EndPoints.Admin.GetMedicineById.HttpResponseMapper;

/// <summary>
///     GetMedicineById extension method
/// </summary>
internal static class GetMedicineByIdHttpResponseMapper
{
    private static GetMedicineByIdHttpResponseManager _GetMedicineByIdHttpResponseManager;

    internal static GetMedicineByIdHttpResponseManager Get()
    {
        return _GetMedicineByIdHttpResponseManager ??= new();
    }
}
