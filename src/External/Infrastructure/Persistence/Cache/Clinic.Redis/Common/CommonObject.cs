using System.Text.Json;

namespace Clinic.Redis.Common;

internal static class CommonObject
{
    internal static readonly JsonSerializerOptions Option =
        new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
}
