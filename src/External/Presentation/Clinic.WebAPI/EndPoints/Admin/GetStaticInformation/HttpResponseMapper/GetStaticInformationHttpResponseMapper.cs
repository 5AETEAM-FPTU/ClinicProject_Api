namespace Clinic.WebAPI.EndPoints.Admin.GetStaticInformation.HttpResponseMapper;

/// <summary>
///     GetStaticInformation extension method
/// </summary>
public class GetStaticInformationHttpResponseMapper
{
    private static GetStaticInformationHttpResponseManager _GetStaticInformationHttpResponseManager;

    internal static GetStaticInformationHttpResponseManager Get()
    {
        return _GetStaticInformationHttpResponseManager ??= new();
    }
}
