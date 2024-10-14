using System;
using System.Collections.Generic;
using Clinic.Application.Features.QueueRooms.RemoveQueueRoom;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.QueueRooms.RemoveQueueRoom.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="RemoveQueueRoomResponse"/> and <see cref="RemoveQueueRoomHttpResponse"/>
/// </summary>
public class RemoveQueueRoomHttpResponseManager
{
    private readonly Dictionary<
        RemoveQueueRoomResponseStatusCode,
        Func<RemoveQueueRoomRequest, RemoveQueueRoomResponse, RemoveQueueRoomHttpResponse>
    > _dictionary;

    internal RemoveQueueRoomHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveQueueRoomResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: RemoveQueueRoomResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveQueueRoomResponseStatusCode.QUEUE_ROOM_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveQueueRoomResponseStatusCode.THIS_UER_IS_NOT_QUEUE_ROOM_OWNER,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveQueueRoomRequest,
        RemoveQueueRoomResponse,
        RemoveQueueRoomHttpResponse
    > Resolve(RemoveQueueRoomResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
