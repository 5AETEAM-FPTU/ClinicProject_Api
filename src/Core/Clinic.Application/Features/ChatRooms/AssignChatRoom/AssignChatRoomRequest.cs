using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ChatRooms.AssignChatRoom;

/// <summary>
///     AssignChatRoom Request.
/// </summary>
public class AssignChatRoomRequest : IFeatureRequest<AssignChatRoomResponse>
{
    public Guid PatientId { get; set; }

    public Guid QueueRoomId { get; set; }

    public string InitialMessage { get; set; }
}
