using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Commons.Abstractions.GetProfileUser;

namespace Clinic.WebAPI.EndPoints.Users.GetProfileUser.HttpResponseMapper;

public class GetProfileUserHttpResponseManager
{
    private readonly Dictionary<
        GetProfileUserResponseStatusCode,
        Func<GetProfileUserRequest, GetProfileUserResponse, GetProfileUserHttpResponse>
    > _dictionary;

    internal GetProfileUserHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetProfileUserResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetProfileUserResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: GetProfileUserResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: GetProfileUserResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetProfileUserResponseStatusCode.FORBIDDEN,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetProfileUserRequest,
        GetProfileUserResponse,
        GetProfileUserHttpResponse
    > Resolve(GetProfileUserResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
