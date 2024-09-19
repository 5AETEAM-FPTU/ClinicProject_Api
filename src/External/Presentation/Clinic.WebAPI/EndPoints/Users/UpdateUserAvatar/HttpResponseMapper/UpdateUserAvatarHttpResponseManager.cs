using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Users.UpdateUserAvatar;


namespace Clinic.WebAPI.EndPoints.Users.UpdateUserAvatar.HttpResponseMapper;

public class UpdateUserAvatarHttpResponseManager
{
    private readonly Dictionary<
        UpdateUserAvatarResponseStatusCode,
        Func<UpdateUserAvatarRequest, UpdateUserAvatarResponse, UpdateUserAvatarHttpResponse>
    > _dictionary;

    internal UpdateUserAvatarHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateUserAvatarResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateUserAvatarResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdateUserAvatarResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateUserAvatarResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdateUserAvatarResponseStatusCode.UPLOAD_IMAGE_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest, //tùy vào nghiệp vụ
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

    }

    internal Func<
        UpdateUserAvatarRequest,
        UpdateUserAvatarResponse,
        UpdateUserAvatarHttpResponse
    > Resolve(UpdateUserAvatarResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
