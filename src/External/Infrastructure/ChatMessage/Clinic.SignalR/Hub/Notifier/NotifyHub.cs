namespace Clinic.SignalR.Hub.Notifier;

using System;
using System.Threading.Tasks;
using Clinic.SignalR.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

/// <summary>
///     Represents a NotifyHub.
/// </summary>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NotifyHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        var userRoles = Context.User?.FindAll("role")?.Select(role => role.Value).ToList();

        if (userRoles.Any(entity => entity.Equals("doctor")))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Constant.GROUP_DOCTOR);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userRoles = Context.User?.FindAll("role")?.Select(role => role.Value).ToList();

        if (userRoles != null && userRoles.Any(entity => entity.Equals("doctor")))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "doctor");
        }

        await base.OnDisconnectedAsync(exception);
    }
}
