using System.Threading.Tasks;

namespace Clinic.Application.Commons.ChatMessage.Messaging;

/// <summary>
///     Represent message handler interface.
/// </summary>
public interface IChatHandler
{
    /// <summary>
    ///     Send message chat.
    /// </summary>
    /// <param name="chatMessage">
    ///     ChatMessage model.
    /// </param>
    /// <returns>
    ///     A string having format of jwt
    ///     or empty string if validate fail.
    /// </returns>
    Task<bool> SendMessageAsync(ChatMessage chatMessage);

    /// <summary>
    ///     Send typing message.
    /// </summary>
    /// <param name="senderId"></param>
    /// <param name="receiverId"></param>
    /// <returns></returns>
    Task SendTypingAsync(string senderId, string receiverId);

    /// <summary>
    ///     Send stop typing message.
    /// </summary>
    /// <param name="senderId"></param>
    /// <param name="receiverId"></param>
    /// <returns></returns>
    Task SendStopTypingAsync(string senderId, string receiverId);

    /// <summary>
    ///     Send removed message.
    /// </summary>
    /// <param name="senderId"></param>
    /// <param name="receiverId"></param>
    /// <returns></returns>
    Task<bool> SendRemovedMessageAsync(string senderId, string receiverId, string chatContentId);
}
