using System;

namespace Clinic.Application.Commons.Constance;

/// <summary>
///     Represent set of constant.
/// </summary>
public static class CommonConstant
{
    public static readonly Guid DEFAULT_ENTITY_ID_AS_GUID = Guid.Parse(
        input: "aa403b53-5ae7-4e50-8b02-92dce06ed1a9"
    );

    public static readonly DateTime MIN_DATE_TIME = DateTime.MinValue.ToUniversalTime();
}
