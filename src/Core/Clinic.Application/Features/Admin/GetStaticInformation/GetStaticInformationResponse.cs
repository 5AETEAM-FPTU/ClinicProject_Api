using Clinic.Application.Commons.Abstractions;


namespace Clinic.Application.Features.Admin.GetStaticInformation;

public class GetStaticInformationResponse : IFeatureResponse
{
    public GetStaticInformationResponseStatusCode StatusCode { get; init; }

    public object ResponseBody { get; init; }

    
}
