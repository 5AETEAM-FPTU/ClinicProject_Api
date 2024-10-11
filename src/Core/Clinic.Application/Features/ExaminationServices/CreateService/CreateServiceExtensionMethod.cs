namespace Clinic.Application.Features.ExaminationServices.CreateService;


/// <summary>
///     CreateService ExtensionMethod
/// </summary>

public static class CreateServiceExtensionMethod
{
    public static string ToAppCode(this CreateServiceResponseStatusCode statusCode)
    {
        return $"{nameof(CreateService)}Feature: {statusCode}";
    }
}
