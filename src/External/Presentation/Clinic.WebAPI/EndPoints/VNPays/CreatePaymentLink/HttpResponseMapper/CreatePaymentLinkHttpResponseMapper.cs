namespace Clinic.WebAPI.EndPoints.CreatePaymentLink.HttpResponseMapper;

internal static class CreatePaymentLinkHttpResponseMapper
{
    private static CreatePaymentLinkHttpResponseManager _manager = new();

    internal static CreatePaymentLinkHttpResponseManager Get() => _manager ??= new();
}
