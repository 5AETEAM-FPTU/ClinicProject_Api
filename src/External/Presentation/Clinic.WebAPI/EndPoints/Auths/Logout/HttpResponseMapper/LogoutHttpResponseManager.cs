using System;
using System.Collections.Generic;
using Clinic.Application.Features.Auths.Logout;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.Logout.HttpResponseMapper;

/// <summary>
///     Mapper for Logout feature
/// </summary>
public class LogoutHttpResponseManager
{
    private readonly Dictionary<
        LogoutResponseStatusCode,
        Func<LogoutRequest, LogoutResponse, LogoutHttpResponse>
    > _dictionary;

    internal LogoutHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: LogoutResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: LogoutResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<LogoutRequest, LogoutResponse, LogoutHttpResponse> Resolve(
        LogoutResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
