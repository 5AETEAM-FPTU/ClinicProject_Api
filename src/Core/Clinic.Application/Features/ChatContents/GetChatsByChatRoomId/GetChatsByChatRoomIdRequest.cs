using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.ChatContents.GetChatsByChatRoomId;

/// <summary>
///     GetChatsByChatRoomId Request
/// </summary>
public class GetChatsByChatRoomIdRequest : IFeatureRequest<GetChatsByChatRoomIdResponse>
{
    [BindFrom("lastReportDate")]
    public DateTime LastReportDate { get; set; }

    public Guid ChatRoomId { get; set; }

    public int PageSize { get; set; } = 8;
}
