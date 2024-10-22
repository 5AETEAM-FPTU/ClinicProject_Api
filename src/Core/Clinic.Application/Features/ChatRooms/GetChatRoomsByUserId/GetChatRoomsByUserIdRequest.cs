using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ChatRooms.GetChatRoomsByUserId;

/// <summary>
///     GetChatRoomsByUserId Request.
/// </summary>
public class GetChatRoomsByUserIdRequest : IFeatureRequest<GetChatRoomsByUserIdResponse>
{
    public DateTime LastConversationTime { get; set; }

    public int PageSize { get; set; } = 10;
}
