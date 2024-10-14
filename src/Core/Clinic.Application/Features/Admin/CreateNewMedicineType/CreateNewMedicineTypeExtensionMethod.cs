namespace Clinic.Application.Features.Admin.CreateNewMedicineType;

public static class CreateNewMedicineTypeExtensionMethod
{
    public static string ToAppCode(this CreateNewMedicineTypeResponseStatusCode statusCode)
    {
        return $"{nameof(CreateNewMedicineType)}Feature: {statusCode}";
    }
}
