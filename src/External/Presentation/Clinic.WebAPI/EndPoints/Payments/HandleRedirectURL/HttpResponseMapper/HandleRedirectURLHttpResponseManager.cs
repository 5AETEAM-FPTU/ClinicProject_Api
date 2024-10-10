﻿using System;
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
