using System;
using System.Collections.Generic;
using Clinic.Application.Commons.VNPay.Response;
using Clinic.Application.Features.VNPays.VNPayIPN;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.VNPayIPN.HttpResponseMapper;

public class VNPayIPNHttpResponseManager
{
    private readonly Dictionary<
        VNPayIPNResponseStatusCode,
        Func<
            VNPayIPNRequest,
            VnpayPayIpnResponse,
            VNPayIPNHttpResponse
        >
    > _dictionary;

    internal VNPayIPNHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: VNPayIPNResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
        _dictionary.Add(
            key: VNPayIPNResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        VNPayIPNRequest,
        VnpayPayIpnResponse,
        VNPayIPNHttpResponse
    > Resolve(VNPayIPNResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
