using System;

namespace Clinic.Application.Commons.Constance;

/// <summary>
///     Represent set of constant.
/// </summary>
public static class CommonConstant
{
    public static readonly Guid DEFAULT_ENTITY_ID_AS_GUID = Guid.Parse(
        input: "00000000-0000-0000-0000-000000000000"
    );

    public static readonly DateTime MIN_DATE_TIME = DateTime.MinValue;

    public static readonly DateTime DATE_NOW_UTC = DateTime.UtcNow;
}
