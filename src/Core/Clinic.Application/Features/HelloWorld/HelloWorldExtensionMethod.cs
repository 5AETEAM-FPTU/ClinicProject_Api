namespace Clinic.Application.Features.HelloWorld;

/// <summary>
///     Extension Method for HelloWorld features.
/// </summary>
public static class HelloWorldExtensionMethod
{
    public static string ToAppCode(this HelloWorldResponseStatusCode statusCode)
    {
        return $"{nameof(HelloWorld)}Feature: {statusCode}";
    }
}
