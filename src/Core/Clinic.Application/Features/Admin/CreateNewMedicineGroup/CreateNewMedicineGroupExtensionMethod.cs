namespace Clinic.Application.Features.Admin.CreateNewMedicineGroup;

public static class CreateNewMedicineGroupExtensionMethod
{
    public static string ToAppCode(this CreateNewMedicineGroupResponseStatusCode statusCode)
    {
        return $"{nameof(CreateNewMedicineGroup)}Feature: {statusCode}";
    }
}
