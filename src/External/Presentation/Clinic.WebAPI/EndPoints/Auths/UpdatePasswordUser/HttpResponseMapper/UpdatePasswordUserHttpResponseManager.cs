using System;
using System.Collections.Generic;
using Clinic.Application.Features.Auths.UpdatePasswordUser;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.UpdatePasswordUser.HttpResponseMapper;

/// <summary>
///     Mapper for UpdatePasswordUser feature
/// </summary>
public class UpdatePasswordUserHttpResponseManager
{
    private readonly Dictionary<
        UpdatePasswordUserResponseStatusCode,
        Func<UpdatePasswordUserRequest, UpdatePasswordUserResponse, UpdatePasswordUserHttpResponse>
    > _dictionary;

    internal UpdatePasswordUserHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdatePasswordUserResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdatePasswordUserResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdatePasswordUserResponseStatusCode.NEW_PASSWORD_IS_NOT_VALID,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdatePasswordUserResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdatePasswordUserResponseStatusCode.USER_IS_NOT_FOUND,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdatePasswordUserRequest,
        UpdatePasswordUserResponse,
        UpdatePasswordUserHttpResponse
    > Resolve(UpdatePasswordUserResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
