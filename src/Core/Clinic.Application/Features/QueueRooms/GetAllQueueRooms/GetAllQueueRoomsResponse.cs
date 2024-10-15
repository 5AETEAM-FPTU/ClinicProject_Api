using System;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;

namespace Clinic.Application.Features.QueueRooms.GetAllQueueRooms;

/// <summary>
///     GetAllQueueRooms Response.
/// </summary>
public sealed class GetAllQueueRoomsResponse : IFeatureResponse
{
    public GetAllQueueRoomsResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<PatientQueue> PatientQueues { get; init; }

        public sealed class PatientQueue
        {
            public Guid QueueRoomId { get; init; }
            public string PatientName { get; init; }
            public string PatientAvatar { get; init; }
            public string Message { get; init; }
        }
    }
}
