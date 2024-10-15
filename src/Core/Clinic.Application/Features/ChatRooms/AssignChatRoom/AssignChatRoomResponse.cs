using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ChatRooms.AssignChatRoom;

/// <summary>
///     AssignChatRoom Response.
/// </summary>
public sealed class AssignChatRoomResponse : IFeatureResponse
{
    public AssignChatRoomResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public Guid AssignChatRoomId { get; init; }
    }
}
