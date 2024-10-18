using System;
using System.Collections.Generic;
using Clinic.Application.Features.ChatRooms.GetChatRoomsByUserId;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ChatRooms.GetChatRoomsByUserId.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="GetChatRoomsByUserIdResponse"/> and <see cref="GetChatRoomsByUserIdHttpResponse"/>
/// </summary>
public class GetChatRoomsByUserIdHttpResponseManager
{
    private readonly Dictionary<
        GetChatRoomsByUserIdResponseStatusCode,
        Func<
            GetChatRoomsByUserIdRequest,
            GetChatRoomsByUserIdResponse,
            GetChatRoomsByUserIdHttpResponse
        >
    > _dictionary;

    internal GetChatRoomsByUserIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetChatRoomsByUserIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetChatRoomsByUserIdResponseStatusCode.ROLE_IS_NOT_PATIENT,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetChatRoomsByUserIdRequest,
        GetChatRoomsByUserIdResponse,
        GetChatRoomsByUserIdHttpResponse
    > Resolve(GetChatRoomsByUserIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
