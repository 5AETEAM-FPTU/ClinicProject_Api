using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.HelloWorld;

/// <summary>
///     HelloWorld response.
/// </summary>
public sealed class HelloWorldResponse : IFeatureResponse
{
    public HelloWorldResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string Description { get; init; }
    }
}
