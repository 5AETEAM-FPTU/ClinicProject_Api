namespace Clinic.WebAPI.EndPoints.Users.GetConsultationOverview.HttpResponseMapper;

/// <summary>
///     GetConsultationOverview extension method
/// </summary>
internal static class GetConsultationOverviewHttpResponseMapper
{
    private static GetConsultationOverviewHttpResponseManager _GetConsultationOverviewHttpResponseManager;

    internal static GetConsultationOverviewHttpResponseManager Get()
    {
        return _GetConsultationOverviewHttpResponseManager ??= new();
    }
}

