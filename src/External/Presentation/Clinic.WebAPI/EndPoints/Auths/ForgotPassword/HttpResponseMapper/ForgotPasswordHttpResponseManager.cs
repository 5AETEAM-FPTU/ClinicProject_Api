using System;
using System.Collections.Generic;
using Clinic.Application.Features.Auths.ForgotPassword;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.ForgotPassword.HttpResponseMapper;

/// <summary>
///     Mapper for ForgotPassword feature
/// </summary>
public class ForgotPasswordHttpResponseManager
{
    private readonly Dictionary<
        ForgotPasswordResponseStatusCode,
        Func<ForgotPasswordRequest, ForgotPasswordResponse, ForgotPasswordHttpResponse>
    > _dictionary;

    internal ForgotPasswordHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: ForgotPasswordResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ForgotPasswordResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: ForgotPasswordResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: ForgotPasswordResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ForgotPasswordResponseStatusCode.USER_WITH_EMAIL_IS_NOT_FOUND,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ForgotPasswordResponseStatusCode.SENDING_USER_RESET_PASSWORD_MAIL_FAIL,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        ForgotPasswordRequest,
        ForgotPasswordResponse,
        ForgotPasswordHttpResponse
    > Resolve(ForgotPasswordResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
