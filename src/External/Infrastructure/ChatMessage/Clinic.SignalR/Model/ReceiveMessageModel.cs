namespace Clinic.SignalR.Model;

/// <summary>
///     Contains information about a received message.
/// </summary>
/// <param name="ChatContentId"></param>
/// <param name="SenderId"></param>
/// <param name="Message"></param>
/// <param name="AssetUrls"></param>
/// <param name="SentAt"></param>
public record ReceiveMessageModel(
    int ChatContentId,
    string SenderId,
    string Message,
    List<string> AssetUrls,
    DateTime SentAt
);
