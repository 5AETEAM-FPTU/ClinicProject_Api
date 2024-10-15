using System;
using System.Collections.Generic;
using Clinic.Application.Features.ChatContents.GetChatsByChatRoomId;
using Clinic.WebAPI.EndPoints.ChatContents.GetChatsByChatRoomId.HttpResponseMapper;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ChatRooms.GetChatsByChatRoomId.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="GetChatsByChatRoomResponse"/> and <see cref="GetChatsByChatRoomHttpResponse"/>
/// </summary>
public class GetChatsByChatRoomIdHttpResponseManager
{
    private readonly Dictionary<
        GetChatsByChatRoomIdResponseStatusCode,
        Func<
            GetChatsByChatRoomIdRequest,
            GetChatsByChatRoomIdResponse,
            GetChatsByChatRoomIdHttpResponse
        >
    > _dictionary;

    internal GetChatsByChatRoomIdHttpResponseManager()
    {
        _dictionary = [];
        _dictionary.Add(
            key: GetChatsByChatRoomIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
        _dictionary.Add(
            key: GetChatsByChatRoomIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetChatsByChatRoomIdResponseStatusCode.USER_CAN_ACCESS_THIS_CHAT,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetChatsByChatRoomIdRequest,
        GetChatsByChatRoomIdResponse,
        GetChatsByChatRoomIdHttpResponse
    > Resolve(GetChatsByChatRoomIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
