using System.Collections.Generic;

namespace Clinic.Application.Commons.ChatMessage.Messaging;

/// <summary>
///     Represent the ChatMessage model.
/// </summary>
public class ChatMessage
{
    public string User { get; set; }
    public string Message { get; set; }
    public ICollection<Urls> FileUrls { get; set; }

    public sealed class Urls
    {
        public string Url { get; set; }
        public string Type { get; set; }
    }
}
