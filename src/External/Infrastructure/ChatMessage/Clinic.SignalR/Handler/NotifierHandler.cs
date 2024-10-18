using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.ChatMessage.Notifier;
using Clinic.Application.Commons.Notifier.Messaging;
using Clinic.SignalR.Common;
using Clinic.SignalR.Hub.Notifier;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Clinic.SignalR.Handler;

/// <summary>
///     This is a sample implementation of INotifierHandler.
/// </summary>
internal class NotifierHandler : INotifierHandler
{
    private readonly IHubContext<NotifyHub> _hubContext;
    private readonly ILogger<NotifierHandler> _logger;

    public NotifierHandler(IHubContext<NotifyHub> hubContext, ILogger<NotifierHandler> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task<bool> SendNotifyAsync(UserRequest userRequestConsultant)
    {
        try
        {
            await _hubContext
                .Clients.Group(Constant.GROUP_DOCTOR)
                .SendAsync("ReceiveNotify", userRequestConsultant);
            return true;
        }
        catch
        {
            _logger.LogInformation("Error sending notify to doctor");
            return false;
        }
    }
}
