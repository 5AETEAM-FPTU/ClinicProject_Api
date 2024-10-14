using System;
using System.Collections.Generic;
using Clinic.Application.Features.QueueRooms.GetQueueRoomByUserId;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.QueueRooms.GetQueueRoomByUserId.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="GetQueueRoomByUserIdResponse"/> and <see cref="GetQueueRoomByUserIdHttpResponse"/>
/// </summary>
public class GetQueueRoomByUserIdHttpResponseManager
{
    private readonly Dictionary<
        GetQueueRoomByUserIdResponseStatusCode,
        Func<
            GetQueueRoomByUserIdRequest,
            GetQueueRoomByUserIdResponse,
            GetQueueRoomByUserIdHttpResponse
        >
    > _dictionary;

    internal GetQueueRoomByUserIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetQueueRoomByUserIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetQueueRoomByUserIdResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetQueueRoomByUserIdRequest,
        GetQueueRoomByUserIdResponse,
        GetQueueRoomByUserIdHttpResponse
    > Resolve(GetQueueRoomByUserIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
