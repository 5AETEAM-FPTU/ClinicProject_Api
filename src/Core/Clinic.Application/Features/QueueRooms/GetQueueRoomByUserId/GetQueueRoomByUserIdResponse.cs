using System;
using System.Collections;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;

namespace Clinic.Application.Features.QueueRooms.GetQueueRoomByUserId;

/// <summary>
///     GetQueueRoomByUserId Response.
/// </summary>
public sealed class GetQueueRoomByUserIdResponse : IFeatureResponse
{
    public GetQueueRoomByUserIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public QueueRoomInformation QueueRoom { get; init; }

        public sealed class QueueRoomInformation
        {
            public Guid QueueRoomId { get; init; }
            public string Title { get; init; }
            public string Description { get; init; }
        }
    }
}
