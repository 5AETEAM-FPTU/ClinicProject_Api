using System.Collections.Generic;

namespace Clinic.Application.Commons.ChatMessage.Messaging;

/// <summary>
///     Represent the ChatMessage model.
/// </summary>
public class ChatMessage
{
    public string ChatRoomId { get; set; }

    public string ChatContentId { get; set; }

    public string SenderId { get; set; }

    public string ReceiverId { get; set; }

    public string Message { get; set; }

    public IEnumerable<string> ImageUrls { get; set; }

    public IEnumerable<string> VideoUrls { get; set; }
}
