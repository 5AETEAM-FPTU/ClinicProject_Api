namespace Clinic.WebAPI.EndPoints.Admin.GetAvailableMedicines.HttpResponseMapper;

/// <summary>
///     GetAvailableMedicines extension method
/// </summary>
internal static class GetAvailableMedicinesHttpResponseMapper
{
    private static GetAvailableMedicinesHttpResponseManager _GetAvailableMedicinesHttpResponseManager;

    internal static GetAvailableMedicinesHttpResponseManager Get()
    {
        return _GetAvailableMedicinesHttpResponseManager ??= new();
    }
}
