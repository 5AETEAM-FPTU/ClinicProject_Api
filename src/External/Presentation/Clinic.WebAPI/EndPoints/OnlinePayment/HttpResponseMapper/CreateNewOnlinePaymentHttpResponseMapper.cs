namespace Clinic.WebAPI.EndPoints.OnlinePayment.HttpResponseMapper;

internal static class CreateNewOnlinePaymentHttpResponseMapper
{
    private static CreateNewOnlinePaymentHttpResponseManager _manager = new();

    internal static CreateNewOnlinePaymentHttpResponseManager Get() => _manager ??= new();
}
