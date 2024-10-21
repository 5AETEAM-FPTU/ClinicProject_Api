namespace Clinic.SignalR.Hub.Chat;

using System.Collections.Concurrent;
using Clinic.Application.Commons.ChatMessage.Messaging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

/// <summary>
///     Represents a ChatHub implementation from SignalR.
/// </summary>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ChatHub : Hub
{
    private static readonly ConcurrentDictionary<string, List<string>> _connections = new();
    private readonly ILogger<ChatHub> _logger;

    private readonly IChatHandler _chatHandler;

    public ChatHub(IChatHandler chatHandler, ILogger<ChatHub> logger)
    {
        _chatHandler = chatHandler;
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
        {
            _logger.LogInformation(
                "User connected: {UserId} with Connection ID: {ConnectionId}",
                userId,
                Context.ConnectionId
            );

            _connections.AddOrUpdate(
                userId,
                _ => new List<string> { Context.ConnectionId },
                (_, connectionIds) =>
                {
                    connectionIds.Add(Context.ConnectionId);
                    return connectionIds;
                }
            );
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;

        if (userId != null && _connections.TryGetValue(userId, out var connectionIds))
        {
            connectionIds.Remove(Context.ConnectionId);
            _logger.LogInformation(
                "User disconnected: {UserId} with Connection ID: {ConnectionId}",
                userId,
                Context.ConnectionId
            );

            if (!connectionIds.Any())
            {
                _connections.TryRemove(userId, out _);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task<bool> SendMessageAsync(ChatMessage chatMessage)
    {
        return await _chatHandler.SendMessageAsync(chatMessage);
    }

    public async Task SendTypingAsync(string senderId, string receiverId)
    {
        await _chatHandler.SendTypingAsync(senderId, receiverId);
    }

    public async Task SendStopTypingAsync(string senderId, string receiverId)
    {
        await _chatHandler.SendStopTypingAsync(senderId, receiverId);
    }

    public async Task SendRemovedMessageAsync(
        string senderId,
        string receiverId,
        string chatContentId
    )
    {
        await _chatHandler.SendRemovedMessageAsync(senderId, receiverId, chatContentId);
    }

    public static List<string>? GetConnectionIds(string userId)
    {
        _connections.TryGetValue(userId, out var connectionIds);
        return connectionIds;
    }

    public static bool IsUserConnected(string userId)
    {
        return _connections.ContainsKey(userId);
    }
}
//var userId = Context.UserIdentifier;

//        if (Equals(objA: _connections, objB: default) || _connections.Count == 0)
//            return;

//        if (userId != null && _connections.TryGetValue(userId, out var connectionIds))
//        {
//            connectionIds.Remove(Context.ConnectionId);
//            _logger.LogInformation(
//                "User disconnected: {UserId} with Connection ID: {ConnectionId}",
//                userId,
//                Context.ConnectionId
//            );

//            if (!connectionIds.Any())
//            {
//                _connections.TryRemove(userId, out _);
//            }
//        }

//        await base.OnDisconnectedAsync(exception);
