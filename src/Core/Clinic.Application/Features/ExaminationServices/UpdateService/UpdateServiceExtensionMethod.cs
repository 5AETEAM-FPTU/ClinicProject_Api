using Clinic.Application.Features.ExaminationServices.CreateService;

namespace Clinic.Application.Features.ExaminationServices.UpdateService;


/// <summary>
///     UpdateService ExtensionMethod
/// </summary>

public static class UpdateServiceExtensionMethod
{
    public static string ToAppCode(this UpdateServiceResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateService)}Feature: {statusCode}";
    }
}
