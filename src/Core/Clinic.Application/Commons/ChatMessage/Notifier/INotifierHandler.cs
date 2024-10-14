using System.Threading.Tasks;
using Clinic.Application.Commons.Notifier.Messaging;

namespace Clinic.Application.Commons.ChatMessage.Notifier;

/// <summary>
///     Represent the notifier interface.
/// </summary>
public interface INotifierHandler
{
    /// <summary>
    ///     Send notify.
    /// </summary>
    /// <param name="chatMessage"></param>
    /// <returns>
    ///     <see langword="true"/> if success, otherwise <see langword="false"/>
    /// </returns>
    Task<bool> SendNotifyAsync(UserRequest userRequestConsultant);
}
