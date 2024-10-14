using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.QueueRooms.CreateQueueRoom;

/// <summary>
///     CreateQueueRoom Request.
/// </summary>
public class CreateQueueRoomRequest : IFeatureRequest<CreateQueueRoomResponse>
{
    public string Title { get; set; }

    public string Message { get; set; }
}
