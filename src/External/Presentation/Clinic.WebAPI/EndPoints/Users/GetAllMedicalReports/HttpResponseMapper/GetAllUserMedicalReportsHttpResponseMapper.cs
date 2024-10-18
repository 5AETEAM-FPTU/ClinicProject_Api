namespace Clinic.WebAPI.EndPoints.Users.GetAllMedicalReports.HttpResponseMapper;

/// <summary>
///     GetAllMedicalReport extension method
/// </summary>
internal static class GetAllUserMedicalReportsHttpResponseMapper
{
    private static GetAllUserMedicalReportsHttpResponseManager _manager;

    internal static GetAllUserMedicalReportsHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}

