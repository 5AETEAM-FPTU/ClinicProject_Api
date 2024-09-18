using System;
using System.Collections.Generic;
using Clinic.Application.Features.Auths.ResendUserRegistrationConfirmedEmail;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Auths.ResendUserRegistrationConfirmedEmail.HttpResponseMapper;

/// <summary>
///     Mapper for ResendUserRegistrationConfirmedEmail feature
/// </summary>
public class ResendUserRegistrationConfirmedEmailHttpResponseManager
{
    private readonly Dictionary<
        ResendUserRegistrationConfirmedEmailResponseStatusCode,
        Func<
            ResendUserRegistrationConfirmedEmailRequest,
            ResendUserRegistrationConfirmedEmailResponse,
            ResendUserRegistrationConfirmedEmailHttpResponse
        >
    > _dictionary;

    internal ResendUserRegistrationConfirmedEmailHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: ResendUserRegistrationConfirmedEmailResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ResendUserRegistrationConfirmedEmailResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: ResendUserRegistrationConfirmedEmailResponseStatusCode.SENDING_USER_CONFIRMATION_MAIL_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: ResendUserRegistrationConfirmedEmailResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: ResendUserRegistrationConfirmedEmailResponseStatusCode.USER_HAS_CONFIRMED_EMAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status409Conflict,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        ResendUserRegistrationConfirmedEmailRequest,
        ResendUserRegistrationConfirmedEmailResponse,
        ResendUserRegistrationConfirmedEmailHttpResponse
    > Resolve(ResendUserRegistrationConfirmedEmailResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
