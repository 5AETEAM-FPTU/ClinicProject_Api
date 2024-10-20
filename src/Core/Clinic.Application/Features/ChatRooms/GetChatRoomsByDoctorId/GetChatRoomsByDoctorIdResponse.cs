using System;
using System.Collections;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ChatRooms.GetChatRoomsByDoctorId;

/// <summary>
///     GetChatRoomsByDoctorId Response.
/// </summary>
public sealed class GetChatRoomsByDoctorIdResponse : IFeatureResponse
{
    public GetChatRoomsByDoctorIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<ChatRoom> ChatRooms { get; init; }

        public sealed class ChatRoom
        {
            public Guid UserId { get; init; }
            public Guid ChatRoomId { get; init; }
            public string FullName { get; init; }
            public string Avatar { get; init; }
            public string Title { get; init; }
            public bool IsEndConversation { get; init; }
            public DateTime LatestMessageTime { get; init; }
        }
    }
}
