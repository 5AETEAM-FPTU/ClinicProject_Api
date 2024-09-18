using System;
using System.Collections.Generic;
using Clinic.Application.Features.Auths.LoginWithGoogle;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.LoginWithGoogle.HttpResponseMapper;

/// <summary>
///     Mapper for LoginWithGoogle feature
/// </summary>
public class LoginWithGoogleHttpResponseManager
{
    private readonly Dictionary<
        LoginWithGoogleResponseStatusCode,
        Func<LoginWithGoogleRequest, LoginWithGoogleResponse, LoginWithGoogleHttpResponse>
    > _dictionary;

    internal LoginWithGoogleHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: LoginWithGoogleResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: LoginWithGoogleResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginWithGoogleResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginWithGoogleResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: LoginWithGoogleResponseStatusCode.EMAIL_IS_NOT_CONFIRMED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        LoginWithGoogleRequest,
        LoginWithGoogleResponse,
        LoginWithGoogleHttpResponse
    > Resolve(LoginWithGoogleResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
