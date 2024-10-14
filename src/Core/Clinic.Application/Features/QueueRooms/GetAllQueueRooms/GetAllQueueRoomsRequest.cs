using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.QueueRooms.GetAllQueueRooms;

/// <summary>
///     GetAllQueueRooms Request.
/// </summary>
public class GetAllQueueRoomsRequest : IFeatureRequest<GetAllQueueRoomsResponse>
{
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 7;
}
