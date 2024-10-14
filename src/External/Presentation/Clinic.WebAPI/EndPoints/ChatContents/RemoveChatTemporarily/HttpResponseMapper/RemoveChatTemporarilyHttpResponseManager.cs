using System;
using System.Collections.Generic;
using Clinic.Application.Features.ChatContents.RemoveChatContentTemporarily;
using Clinic.WebAPI.EndPoints.ChatContents.RemoveChatTemporarily.HttpResponseMapper;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ChatRooms.RemoveChatTemporarily.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="RemoveChatTemporarilyResponse"/> and <see cref="RemoveChatTemporarilyHttpResponse"/>
/// </summary>
public class RemoveChatTemporarilyHttpResponseManager
{
    private readonly Dictionary<
        RemoveChatContentTemporarilyResponseStatusCode,
        Func<
            RemoveChatContentTemporarilyRequest,
            RemoveChatContentTemporarilyResponse,
            RemoveChatTemporarilyHttpResponse
        >
    > _dictionary;

    internal RemoveChatTemporarilyHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveChatContentTemporarilyResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: RemoveChatContentTemporarilyResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveChatContentTemporarilyResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveChatContentTemporarilyResponseStatusCode.CHATCONTENT_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveChatContentTemporarilyResponseStatusCode.CHATCONTENT_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveChatContentTemporarilyRequest,
        RemoveChatContentTemporarilyResponse,
        RemoveChatTemporarilyHttpResponse
    > Resolve(RemoveChatContentTemporarilyResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
