using System;
using System.Collections.Generic;
using Clinic.Application.Features.Auths.ConfirmUserRegistrationEmail;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.ConfirmUserRegistrationEmail.HttpResponseMapper;

/// <summary>
///     Mapper for ConfirmUserRegistrationEmail feature
/// </summary>
public class ConfirmUserRegistrationEmailHttpResponseManager
{
    private readonly Dictionary<
        ConfirmUserRegistrationEmailResponseStatusCode,
        Func<
            ConfirmUserRegistrationEmailRequest,
            ConfirmUserRegistrationEmailResponse,
            ConfirmUserRegistrationEmailHttpResponse
        >
    > _dictionary;

    internal ConfirmUserRegistrationEmailHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: ConfirmUserRegistrationEmailResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ConfirmUserRegistrationEmailResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: ConfirmUserRegistrationEmailResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status423Locked,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ConfirmUserRegistrationEmailResponseStatusCode.USER_HAS_CONFIRMED_REGISTRATION_EMAIL,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ConfirmUserRegistrationEmailResponseStatusCode.USER_IS_NOT_FOUND,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ConfirmUserRegistrationEmailResponseStatusCode.TOKEN_IS_NOT_CORRECT,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        ConfirmUserRegistrationEmailRequest,
        ConfirmUserRegistrationEmailResponse,
        ConfirmUserRegistrationEmailHttpResponse
    > Resolve(ConfirmUserRegistrationEmailResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
