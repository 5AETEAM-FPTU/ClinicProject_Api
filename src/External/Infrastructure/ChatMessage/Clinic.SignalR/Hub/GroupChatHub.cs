namespace Clinic.SignalR.Hub;

using System.Collections.Concurrent;
using Clinic.Application.Commons.ChatMessage.Messaging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

/// <summary>
///     Represents a ChatHub implementation from SignalR.
/// </summary>
///
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ChatHub : Hub
{
    private static readonly ConcurrentDictionary<string, List<string>> _connections = new();

    private readonly IChatHandler _chatHandler;

    public ChatHub(IChatHandler chatHandler)
    {
        _chatHandler = chatHandler;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        if (userId != null && _connections.TryGetValue(userId, out var connectionIds))
        {
            connectionIds.Remove(Context.ConnectionId);
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
