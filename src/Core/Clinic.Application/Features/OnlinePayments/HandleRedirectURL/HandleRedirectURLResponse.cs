using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.OnlinePayments.HandleRedirectURL;

/// <summary>
///     HandleRedirectURL Response.
/// </summary>
public class HandleRedirectURLResponse : IFeatureResponse
{
    public HandleRedirectURLResponseStatusCode StatusCode { get; init; }
}
