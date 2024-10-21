using System;
using System.Collections.Generic;
using Clinic.Application.Features.OnlinePayments.HandleRedirectURL;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Payments.HandleRedirectURL.HttpResponseMapper;

/// <summary>
///     Manages the mapping <see cref="HandleRedirectURLResponse"/>
/// </summary>
public class HandleRedirectURLHttpResponseManager
{
    private readonly Dictionary<
        HandleRedirectURLResponseStatusCode,
        Func<HandleRedirectURLRequest, HandleRedirectURLResponse, HandleRedirectURLHttpResponse>
    > _dictionary;

    internal HandleRedirectURLHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: HandleRedirectURLResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: HandleRedirectURLResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: HandleRedirectURLResponseStatusCode.PAYMENT_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: HandleRedirectURLResponseStatusCode.PAYMENT_IS_ALREADY_PAID,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status409Conflict,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: HandleRedirectURLResponseStatusCode.RETURN_CANCEL_PAYMENT,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status202Accepted,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        HandleRedirectURLRequest,
        HandleRedirectURLResponse,
        HandleRedirectURLHttpResponse
    > Resolve(HandleRedirectURLResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
