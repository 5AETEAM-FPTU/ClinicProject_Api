using System;
using System.Collections.Generic;
using Clinic.Application.Features.Auths.RefreshAccessToken;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.RefreshAccessToken.HttpResponseMapper;

/// <summary>
///     Mapper for RefreshAccessToken feature
/// </summary>
public class RefreshAccessTokenHttpResponseManager
{
    private readonly Dictionary<
        RefreshAccessTokenResponseStatusCode,
        Func<RefreshAccessTokenRequest, RefreshAccessTokenResponse, RefreshAccessTokenHttpResponse>
    > _dictionary;

    internal RefreshAccessTokenHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RefreshAccessTokenResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: RefreshAccessTokenResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
        _dictionary.Add(
            key: RefreshAccessTokenResponseStatusCode.REFRESH_TOKEN_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
        _dictionary.Add(
            key: RefreshAccessTokenResponseStatusCode.REFRESH_TOKEN_IS_EXPIRED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        RefreshAccessTokenRequest,
        RefreshAccessTokenResponse,
        RefreshAccessTokenHttpResponse
    > Resolve(RefreshAccessTokenResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
