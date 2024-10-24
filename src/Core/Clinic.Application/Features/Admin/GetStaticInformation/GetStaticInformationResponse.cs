using Clinic.Application.Commons.Abstractions;


namespace Clinic.Application.Features.Admin.GetStaticInformation;

public class GetStaticInformationResponse : IFeatureResponse
{
    public GetStaticInformationResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public int CountAppointment { get; init; }

    }
}
