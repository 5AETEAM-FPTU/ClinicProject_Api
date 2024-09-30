using System;
using System.Collections.Generic;
using Clinic.Application.Features.Auths.ChangingPassword;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.ChangingPassword.HttpResponseMapper;

/// <summary>
///     Mapper for ChangingPassword feature
/// </summary>
public class ChangingPasswordHttpResponseManager
{
    private readonly Dictionary<
        ChangingPasswordResponseStatusCode,
        Func<ChangingPasswordRequest, ChangingPasswordResponse, ChangingPasswordHttpResponse>
    > _dictionary;

    internal ChangingPasswordHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: ChangingPasswordResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ChangingPasswordResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ChangingPasswordResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ChangingPasswordResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ChangingPasswordResponseStatusCode.NEW_PASSWORD_IS_NOT_VALID,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ChangingPasswordResponseStatusCode.RESET_PASSWORD_TOKEN_IS_NOT_FOUND,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ChangingPasswordResponseStatusCode.EMAIL_IS_NOT_MATCH_WITH_OTP,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ChangingPasswordResponseStatusCode.OTP_CODE_IS_EXPIRED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        ChangingPasswordRequest,
        ChangingPasswordResponse,
        ChangingPasswordHttpResponse
    > Resolve(ChangingPasswordResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
