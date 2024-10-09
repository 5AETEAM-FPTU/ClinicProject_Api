﻿using System;
using System.Collections.Generic;
using Clinic.Application.Features.Users.VNPays.CreatePaymentLink;
using Clinic.Application.Features.VNPays.CreatePaymentLink;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.CreatePaymentLink.HttpResponseMapper;

public class CreatePaymentLinkHttpResponseManager
{
    private readonly Dictionary<
        CreatePaymentLinkResponseStatusCode,
        Func<
            CreatePaymentLinkRequest,
            CreatePaymentLinkResponse,
            CreatePaymentLinkHttpResponse
        >
    > _dictionary;

    internal CreatePaymentLinkHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreatePaymentLinkResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
        _dictionary.Add(
            key: CreatePaymentLinkResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        CreatePaymentLinkRequest,
        CreatePaymentLinkResponse,
        CreatePaymentLinkHttpResponse
    > Resolve(CreatePaymentLinkResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}