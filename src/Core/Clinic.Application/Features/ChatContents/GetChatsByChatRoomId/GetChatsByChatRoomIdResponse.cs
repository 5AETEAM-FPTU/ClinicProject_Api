using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ChatContents.GetChatsByChatRoomId;

/// <summary>
///     GetChatsByChatRoomId Response
/// </summary>
public class GetChatsByChatRoomIdResponse : IFeatureResponse
{
    public GetChatsByChatRoomIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<Message> Messages { get; init; }

        public class Message
        {
            public Guid ChatContentId { get; set; }
            public Guid SenderId { get; set; }
            public string Content { get; set; }
            public DateTime Time { get; set; }
            public bool IsRemoved { get; set; }
            public IEnumerable<string> AssetUrl { get; set; }
        }
    }
}
