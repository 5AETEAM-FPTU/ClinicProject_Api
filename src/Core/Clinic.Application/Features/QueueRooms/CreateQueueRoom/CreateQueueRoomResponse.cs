using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.QueueRooms.CreateQueueRoom;

/// <summary>
///     CreateQueueRoom Response.
/// </summary>
public sealed class CreateQueueRoomResponse : IFeatureResponse
{
    public CreateQueueRoomResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public Guid CreateQueueRoomId { get; init; }
    }
}
