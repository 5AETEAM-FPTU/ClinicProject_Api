using System;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;

namespace Clinic.Application.Features.QueueRooms.RemoveQueueRoom;

/// <summary>
///     RemoveQueueRoom Response.
/// </summary>
public sealed class RemoveQueueRoomResponse : IFeatureResponse
{
    public RemoveQueueRoomResponseStatusCode StatusCode { get; init; }
}
