using System;
using System.Collections.Generic;
using Clinic.Application.Features.ChatRooms.SwitchToEndChatRoom;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ChatRooms.SwitchToEndChatRoom.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="SwitchToEndChatRoomResponse"/> and <see cref="SwitchToEndChatRoomHttpResponse"/>
/// </summary>
public class SwitchToEndChatRoomHttpResponseManager
{
    private readonly Dictionary<
        SwitchToEndChatRoomResponseStatusCode,
        Func<
            SwitchToEndChatRoomRequest,
            SwitchToEndChatRoomResponse,
            SwitchToEndChatRoomHttpResponse
        >
    > _dictionary;

    internal SwitchToEndChatRoomHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: SwitchToEndChatRoomResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: SwitchToEndChatRoomResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: SwitchToEndChatRoomResponseStatusCode.ROLE_IS_NOT_DOCTOR_OR_STAFF,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        SwitchToEndChatRoomRequest,
        SwitchToEndChatRoomResponse,
        SwitchToEndChatRoomHttpResponse
    > Resolve(SwitchToEndChatRoomResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
