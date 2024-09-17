using System;
using System.Collections.Generic;
using Clinic.Application.Features.Auths.RegisterAsUser;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.RegisterAsUser.HttpResponseMapper;

/// <summary>
///     Mapper for RegisterAsUser feature
/// </summary>
public class RegisterAsUserHttpResponseManager
{
    private readonly Dictionary<
        RegisterAsUserResponseStatusCode,
        Func<RegisterAsUserRequest, RegisterAsUserResponse, RegisterAsUserHttpResponse>
    > _dictionary;

    internal RegisterAsUserHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RegisterAsUserResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RegisterAsUserResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
        _dictionary.Add(
            key: RegisterAsUserResponseStatusCode.USERNAME_IS_EXIST,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status409Conflict,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: RegisterAsUserResponseStatusCode.SENDING_USER_CONFIRMATION_MAIL_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: RegisterAsUserResponseStatusCode.EMAIL_IS_EXIST,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status409Conflict,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: RegisterAsUserResponseStatusCode.PASSWORD_IS_NOT_VAID,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        RegisterAsUserRequest,
        RegisterAsUserResponse,
        RegisterAsUserHttpResponse
    > Resolve(RegisterAsUserResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
