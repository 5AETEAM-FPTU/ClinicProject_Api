using Clinic.Application.Commons.ChatMessage.Messaging;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ChatContents.CreateChatContent;
using Clinic.Domain.Features.UnitOfWorks;
using Clinic.SignalR.Hub.Chat;
using Microsoft.AspNetCore.SignalR;

namespace Clinic.SignalR.Handler;

/// <summary>
///     This is a sample implementation of IChatHandler
/// </summary>
public class ChatHandler : IChatHandler
{
    private readonly IHubContext<ChatHub> _hubContext;
    private IUnitOfWork _unitOfWork;

    public ChatHandler(IHubContext<ChatHub> hubContext, IUnitOfWork unitOfWork)
    {
        _hubContext = hubContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> SendMessageAsync(ChatMessage chatMessage)
    {
        //var isChatRoomExperid =
        //    await _unitOfWork.CreateChatContentRepository.IsChatRoomExperiedQueryAsync(
        //        chatRoomId: Guid.Parse(input: chatMessage.ChatRoomId),
        //        cancellationToken: default
        //    );

        //if (isChatRoomExperid)
        //{
        //    return false;
        //}

        var chatContentId = Guid.NewGuid();
        var chatContent = new ChatContent()
        {
            Id = Guid.Parse(input: chatMessage.ChatContentId),
            TextContent = chatMessage.Message,
            Assets = chatMessage
                .ImageUrls.Select(asset => new Asset()
                {
                    Id = Guid.NewGuid(),
                    FilePath = asset,
                    FileName = asset,
                    Type = "image",
                })
                .ToList(),
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.Parse(input: chatMessage.SenderId),
            SenderId = Guid.Parse(input: chatMessage.SenderId),
            ChatRoomId = Guid.Parse(input: chatMessage.ChatRoomId),
        };

        var dbResult = await _unitOfWork.CreateChatContentRepository.AddChatContentCommandAsync(
            chatContent,
            cancellationToken: default
        );

        if (!dbResult)
        {
            return false;
        }

        try
        {
            var connectIdReceivers = ChatHub.GetConnectionIds(chatMessage.ReceiverId);

            var connectIdSenders = ChatHub.GetConnectionIds(chatMessage.SenderId);

            if (Equals(connectIdSenders, default))
            {
                return false;
            }

            if (!Equals(connectIdReceivers, default))
            {
                connectIdSenders?.AddRange(connectIdReceivers);
            }

            await _hubContext
                .Clients.Clients(connectIdSenders)
                .SendAsync(
                    "ReceiveMessage",
                    chatMessage.ChatContentId,
                    chatMessage.SenderId,
                    chatMessage.Message,
                    chatMessage.ChatRoomId,
                    chatMessage.ImageUrls,
                    chatMessage.VideoUrls,
                    DateTime.Now
                );

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending message: {ex.Message}");
            return false;
        }
    }

    public async Task SendTypingAsync(string senderId, string receiverId)
    {
        var connectIdReceivers = ChatHub.GetConnectionIds(receiverId);

        await _hubContext.Clients.Clients(connectIdReceivers).SendAsync("ReceiveTyping", senderId);
    }

    public async Task SendStopTypingAsync(string senderId, string receiverId)
    {
        var connectIdReceivers = ChatHub.GetConnectionIds(receiverId);

        await _hubContext
            .Clients.Clients(connectIdReceivers)
            .SendAsync("ReceiveStopTyping", senderId);
    }

    public async Task<bool> SendRemovedMessageAsync(
        string senderId,
        string receiverId,
        string chatContentId
    )
    {
        var connectIdReceivers = ChatHub.GetConnectionIds(receiverId);

        var connectIdSenders = ChatHub.GetConnectionIds(senderId);

        if (Equals(connectIdSenders, default))
        {
            return false;
        }

        if (!Equals(connectIdReceivers, default))
        {
            connectIdSenders?.AddRange(connectIdReceivers);
        }

        await _hubContext
            .Clients.Clients(connectIdSenders)
            .SendAsync("ReceiveRemovedMessage", senderId, chatContentId);

        return true;
    }
}

























//await _hubContext
//    .Clients.Group(chatMessage.ReceiverId)
//    .SendAsync(
//        "ReceiveMessage",
//        chatMessage.SenderId,
//        chatMessage.Message,
//        chatMessage.ImageUrls,
//        chatMessage.VideoUrls,
//        DateTime.Now
//    );
