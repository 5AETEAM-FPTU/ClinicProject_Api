using System;
using System.Collections.Generic;
using Clinic.Application.Features.QueueRooms.GetAllQueueRooms;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.QueueRooms.GetAllQueueRooms.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="GetAllQueueRoomsResponse"/> and <see cref="GetAllQueueRoomsHttpResponse"/>
/// </summary>
public class GetAllQueueRoomsHttpResponseManager
{
    private readonly Dictionary<
        GetAllQueueRoomsResponseStatusCode,
        Func<GetAllQueueRoomsRequest, GetAllQueueRoomsResponse, GetAllQueueRoomsHttpResponse>
    > _dictionary;

    internal GetAllQueueRoomsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllQueueRoomsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetAllQueueRoomsResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAllQueueRoomsRequest,
        GetAllQueueRoomsResponse,
        GetAllQueueRoomsHttpResponse
    > Resolve(GetAllQueueRoomsResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
