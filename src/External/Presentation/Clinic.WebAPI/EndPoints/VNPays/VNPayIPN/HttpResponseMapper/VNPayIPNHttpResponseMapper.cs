namespace Clinic.WebAPI.EndPoints.VNPayIPN.HttpResponseMapper;

internal static class VNPayIPNHttpResponseMapper
{
    private static VNPayIPNHttpResponseManager _manager = new();

    internal static VNPayIPNHttpResponseManager Get() => _manager ??= new();
}
