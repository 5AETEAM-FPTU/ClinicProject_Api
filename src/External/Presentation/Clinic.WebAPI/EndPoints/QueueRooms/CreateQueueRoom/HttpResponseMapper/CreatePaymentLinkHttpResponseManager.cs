using System;
using System.Collections.Generic;
using Clinic.Application.Features.QueueRooms.CreateQueueRoom;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.QueueRooms.CreateQueueRoom.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="CreateQueueRoomResponse"/> and <see cref="CreateQueueRoomHttpResponse"/>
/// </summary>
public class CreateQueueRoomHttpResponseManager
{
    private readonly Dictionary<
        CreateQueueRoomResponseStatusCode,
        Func<CreateQueueRoomRequest, CreateQueueRoomResponse, CreateQueueRoomHttpResponse>
    > _dictionary;

    internal CreateQueueRoomHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateQueueRoomResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
        _dictionary.Add(
            key: CreateQueueRoomResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateQueueRoomResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateQueueRoomResponseStatusCode.USER_HAVE_BEEN_ANOTHER_REQUEST_CONSULTANT,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateQueueRoomResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        CreateQueueRoomRequest,
        CreateQueueRoomResponse,
        CreateQueueRoomHttpResponse
    > Resolve(CreateQueueRoomResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
