using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.ChatRooms.SwitchToEndChatRoom;

/// <summary>
///     SwitchToEndChatRoom Request.
/// </summary>
public class SwitchToEndChatRoomRequest : IFeatureRequest<SwitchToEndChatRoomResponse>
{
    [BindFrom("chatRoomId")]
    public Guid ChatRoomId { get; set; }
}
