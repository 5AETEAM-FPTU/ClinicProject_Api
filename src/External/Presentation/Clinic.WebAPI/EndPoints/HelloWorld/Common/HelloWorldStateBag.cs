namespace Clinic.WebAPI.EndPoints.HelloWorld.Common;

/// <summary>
///     Represents the HelloWorld state bag.
/// </summary>
internal sealed class HelloWorldStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;
}
