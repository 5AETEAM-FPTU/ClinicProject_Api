using System;
using System.Collections.Generic;
using Clinic.Application.Features.ChatRooms.GetChatRoomsByDoctorId;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ChatRooms.GetChatRoomsByDoctorId.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="GetChatRoomsByDoctorIdResponse"/> and <see cref="GetChatRoomsByDoctorIdHttpResponse"/>
/// </summary>
public class GetChatRoomsByDoctorIdHttpResponseManager
{
    private readonly Dictionary<
        GetChatRoomsByDoctorIdResponseStatusCode,
        Func<
            GetChatRoomsByDoctorIdRequest,
            GetChatRoomsByDoctorIdResponse,
            GetChatRoomsByDoctorIdHttpResponse
        >
    > _dictionary;

    internal GetChatRoomsByDoctorIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetChatRoomsByDoctorIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetChatRoomsByDoctorIdResponseStatusCode.ROLE_IS_NOT_DOCTOR,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetChatRoomsByDoctorIdRequest,
        GetChatRoomsByDoctorIdResponse,
        GetChatRoomsByDoctorIdHttpResponse
    > Resolve(GetChatRoomsByDoctorIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
