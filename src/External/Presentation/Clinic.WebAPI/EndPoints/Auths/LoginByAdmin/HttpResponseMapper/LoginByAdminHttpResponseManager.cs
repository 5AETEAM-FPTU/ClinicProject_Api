using System;
using System.Collections.Generic;
using Clinic.Application.Features.Auths.LoginByAdmin;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.LoginByAdmin.HttpResponseMapper;

/// <summary>
///     Mapper for LoginByAdmin feature
/// </summary>
public class LoginByAdminHttpResponseManager
{
    private readonly Dictionary<
        LoginByAdminResponseStatusCode,
        Func<LoginByAdminRequest, LoginByAdminResponse, LoginByAdminHttpResponse>
    > _dictionary;

    internal LoginByAdminHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: LoginByAdminResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: LoginByAdminResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginByAdminResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginByAdminResponseStatusCode.USER_IS_LOCKED_OUT,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status429TooManyRequests,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginByAdminResponseStatusCode.USER_PASSWORD_IS_NOT_CORRECT,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginByAdminResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginByAdminResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: LoginByAdminResponseStatusCode.EMAIL_IS_NOT_CONFIRMED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: LoginByAdminResponseStatusCode.ROLE_IS_NOT_ADMIN,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<LoginByAdminRequest, LoginByAdminResponse, LoginByAdminHttpResponse> Resolve(
        LoginByAdminResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
