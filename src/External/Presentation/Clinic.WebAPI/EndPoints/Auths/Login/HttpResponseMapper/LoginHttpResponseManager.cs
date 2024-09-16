using System;
using System.Collections.Generic;
using Clinic.Application.Features.Auths.Login;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.Login.HttpResponseMapper;

/// <summary>
///     Mapper for Login feature
/// </summary>
public class LoginHttpResponseManager
{
    private readonly Dictionary<
        LoginResponseStatusCode,
        Func<LoginRequest, LoginResponse, LoginHttpResponse>
    > _dictionary;

    internal LoginHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: LoginResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: LoginResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<LoginRequest, LoginResponse, LoginHttpResponse> Resolve(
        LoginResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
