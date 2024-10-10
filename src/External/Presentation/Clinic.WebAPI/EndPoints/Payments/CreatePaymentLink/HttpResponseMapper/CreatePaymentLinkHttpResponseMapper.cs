namespace Clinic.WebAPI.EndPoints.Payments.CreatePaymentLink.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="CreatePaymentLinkResponse"/> and <see cref="CreatePaymentLinkHttpResponse"/>
/// </summary>
internal static class CreatePaymentLinkHttpResponseMapper
{
    private static CreatePaymentLinkHttpResponseManager _manager = new();

    internal static CreatePaymentLinkHttpResponseManager Get() => _manager ??= new();
}
