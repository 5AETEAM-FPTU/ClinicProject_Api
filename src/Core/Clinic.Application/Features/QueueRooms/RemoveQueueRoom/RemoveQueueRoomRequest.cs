using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.QueueRooms.RemoveQueueRoom;

/// <summary>
///     RemoveQueueRoom Request.
/// </summary>
public class RemoveQueueRoomRequest : IFeatureRequest<RemoveQueueRoomResponse>
{
    [BindFrom("queueRoomId")]
    public Guid QueueRoomId { get; set; }
}
