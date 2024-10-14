namespace Clinic.WebAPI.EndPoints.Admin.CreateNewMedicineGroup.HttpResponseMapper;

/// <summary>
///     CreateNewMedicineGroup extension method
/// </summary>
internal static class CreateNewMedicineGroupHttpResponseMapper
{
    private static CreateNewMedicineGroupHttpResponseManager _CreateNewMedicineGroupHttpResponseManager;

    internal static CreateNewMedicineGroupHttpResponseManager Get()
    {
        return _CreateNewMedicineGroupHttpResponseManager ??= new();
    }
}
