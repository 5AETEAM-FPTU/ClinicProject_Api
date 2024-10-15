using System;
using System.Collections.Generic;
using Clinic.Application.Features.ChatRooms.AssignChatRoom;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ChatRooms.AssignChatRoom.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="AssignChatRoomResponse"/> and <see cref="AssignChatRoomHttpResponse"/>
/// </summary>
public class AssignChatRoomHttpResponseManager
{
    private readonly Dictionary<
        AssignChatRoomResponseStatusCode,
        Func<AssignChatRoomRequest, AssignChatRoomResponse, AssignChatRoomHttpResponse>
    > _dictionary;

    internal AssignChatRoomHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: AssignChatRoomResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
        _dictionary.Add(
            key: AssignChatRoomResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: AssignChatRoomResponseStatusCode.PATIENT_ID_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: AssignChatRoomResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        AssignChatRoomRequest,
        AssignChatRoomResponse,
        AssignChatRoomHttpResponse
    > Resolve(AssignChatRoomResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
