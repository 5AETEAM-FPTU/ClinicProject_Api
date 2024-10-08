namespace Clinic.Application.Features.Admin.CreateMedicine;

public static class CreateMedicineExtensionMethod
{
    public static string ToAppCode(this CreateMedicineResponseStatusCode statusCode)
    {
        return $"{nameof(CreateMedicine)}Feature: {statusCode}";
    }
}
