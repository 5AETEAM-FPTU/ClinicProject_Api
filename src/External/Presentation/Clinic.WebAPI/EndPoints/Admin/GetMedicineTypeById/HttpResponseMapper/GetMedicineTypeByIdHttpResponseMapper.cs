namespace Clinic.WebAPI.EndPoints.Admin.GetMedicineTypeById.HttpResponseMapper;

/// <summary>
///     GetMedicineTypeById extension method
/// </summary>
internal static class GetMedicineTypeByIdHttpResponseMapper
{
    private static GetMedicineTypeByIdHttpResponseManager _GetMedicineTypeByIdHttpResponseManager;

    internal static GetMedicineTypeByIdHttpResponseManager Get()
    {
        return _GetMedicineTypeByIdHttpResponseManager ??= new();
    }
}

