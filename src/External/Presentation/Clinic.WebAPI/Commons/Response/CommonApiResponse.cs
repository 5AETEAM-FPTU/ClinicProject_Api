using System;
using System.Collections.Generic;
using Clinic.WebAPI.Commons.AppCodes;

namespace Clinic.WebAPI.Commons.Response;

/// <summary>
///     Contain common response for all api.
/// </summary>
/// <remarks>
///     All http responses format must be this format.
/// </remarks>
internal sealed class CommonApiResponse
{
    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public string AppCode { get; init; } = CommonAppCode.SUCCESS.ToString();

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
