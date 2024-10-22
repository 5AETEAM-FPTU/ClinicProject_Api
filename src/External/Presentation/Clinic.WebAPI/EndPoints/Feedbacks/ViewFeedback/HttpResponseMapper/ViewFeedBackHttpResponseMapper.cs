namespace Clinic.WebAPI.EndPoints.Feedbacks.ViewFeedback.HttpResponseMapper;

/// <summary>
///     ViewFeedback extension method
/// </summary>
internal static class ViewFeedBackHttpResponseMapper
{
    private static ViewFeedBackHttpResponseManager _sendFeedBackHttpResponseManager;

    internal static ViewFeedBackHttpResponseManager Get()
    {
        return _sendFeedBackHttpResponseManager ??= new();
    }
}
