using Clinic.Application.Commons.ChatMessage.Messaging;
using Clinic.SignalR.Hub;
using Microsoft.AspNetCore.SignalR;

namespace Clinic.SignalR.Handler;

/// <summary>
///     This is a sample implementation of IChatHandler
/// </summary>
public class ChatHandler : IChatHandler
{
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatHandler(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task<bool> SendMessageAsync(ChatMessage chatMessage)
    {
        try
        {
            var connectIdReceivers = ChatHub.GetConnectionIds(chatMessage.ReceiverId);
            var connectIdSenders = ChatHub.GetConnectionIds(chatMessage.SenderId);

            await _hubContext
                .Clients.Group(chatMessage.ReceiverId)
                .SendAsync(
                    "ReceiveMessage",
                    chatMessage.SenderId,
                    chatMessage.Message,
                    chatMessage.ImageUrls,
                    chatMessage.VideoUrls,
                    DateTime.Now
                );

            //await _hubContext
            //    .Clients.Clients(connectIdSenders.Concat(connectIdReceivers))
            //    .SendAsync(
            //        "ReceiveMessage",
            //        chatMessage.SenderId,
            //        chatMessage.Message,
            //        chatMessage.ImageUrls,
            //        chatMessage.VideoUrls,
            //        DateTime.Now
            //    );
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending message: {ex.Message}");
            return false;
        }
    }
}
