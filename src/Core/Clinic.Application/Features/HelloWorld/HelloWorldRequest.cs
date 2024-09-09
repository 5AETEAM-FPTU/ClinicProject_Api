using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.HelloWorld;

/// <summary>
///     HelloWorld Request
/// </summary>
public class HelloWorldRequest : IFeatureRequest<HelloWorldResponse>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
}
