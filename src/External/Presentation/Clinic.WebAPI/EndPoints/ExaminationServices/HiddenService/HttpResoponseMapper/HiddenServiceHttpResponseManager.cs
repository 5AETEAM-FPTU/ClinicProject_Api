using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.ExaminationServices.HiddenService;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.HiddenService.HttpResoponseMapper;

/// <summary>
///     Mapper for  HiddenService feature
/// </summary>
public class HiddenServiceHttpResponseManager
{
    private readonly Dictionary<
        HiddenServiceResponseStatusCode,
        Func<HiddenServiceRequest, HiddenServiceResponse, HiddenServiceHttpResponse>
    > _dictionary;

    internal HiddenServiceHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: HiddenServiceResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: HiddenServiceResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: HiddenServiceResponseStatusCode.FORBIDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: HiddenServiceResponseStatusCode.SERVICE_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        HiddenServiceRequest,
        HiddenServiceResponse,
        HiddenServiceHttpResponse
    > Resolve(HiddenServiceResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}

