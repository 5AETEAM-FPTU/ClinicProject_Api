using System.Threading.Tasks;

namespace Clinic.Application.Commons.ChatMessage.Messaging;

/// <summary>
///     Represent message handler interface.
/// </summary>
public interface IMessagingHandler
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
}
