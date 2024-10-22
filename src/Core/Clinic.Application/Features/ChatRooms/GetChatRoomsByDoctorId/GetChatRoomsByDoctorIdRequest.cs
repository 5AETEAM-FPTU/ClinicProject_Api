using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.ChatRooms.GetChatRoomsByDoctorId;

/// <summary>
///     GetChatRoomsByDoctorId Request.
/// </summary>
public class GetChatRoomsByDoctorIdRequest : IFeatureRequest<GetChatRoomsByDoctorIdResponse>
{
    public DateTime LastConversationTime { get; set; }

    public int PageSize { get; set; } = 10;
}
