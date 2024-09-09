namespace Clinic.WebAPI.EndPoints.HelloWorld.HttpResponseMapper;

/// <summary>
///     HelloWorld extension method
/// </summary>
internal static class HelloWorldHttpResponseMapper
{
    private static HelloWorldHttpResponseManager _HelloWorldHttpResponseManager;

    internal static HelloWorldHttpResponseManager Get()
    {
        return _HelloWorldHttpResponseManager ??= new();
    }
}
