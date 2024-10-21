namespace Clinic.WebAPI.EndPoints.Users.SendFeedBack.HttpResponseMapper;

/// <summary>
///     SendFeedBack extension method
/// </summary>
internal static class SendFeedBackHttpResponseMapper
{
    private static SendFeedBackHttpResponseManager _sendFeedBackHttpResponseManager;

    internal static SendFeedBackHttpResponseManager Get()
    {
        return _sendFeedBackHttpResponseManager ??= new();
    }
}
