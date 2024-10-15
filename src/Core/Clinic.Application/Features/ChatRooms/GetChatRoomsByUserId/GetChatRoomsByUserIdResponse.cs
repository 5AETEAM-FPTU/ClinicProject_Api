using System;
using System.Collections;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ChatRooms.GetChatRoomsByUserId;

/// <summary>
///     GetChatRoomsByUserId Response.
/// </summary>
public sealed class GetChatRoomsByUserIdResponse : IFeatureResponse
{
    public GetChatRoomsByUserIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<ChatRoom> ChatRooms { get; init; }

        public sealed class ChatRoom
        {
            public Guid ChatRoomId { get; init; }
            public string FullName { get; init; }
            public string Avatar { get; init; }
            public bool IsEndConversation { get; init; }
        }
    }
}
