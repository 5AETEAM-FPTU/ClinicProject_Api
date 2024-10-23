using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ChatRooms.SwitchToEndChatRoom;

/// <summary>
///     SwitchToEndChatRoom Response.
/// </summary>
public sealed class SwitchToEndChatRoomResponse : IFeatureResponse
{
    public SwitchToEndChatRoomResponseStatusCode StatusCode { get; init; }
}
