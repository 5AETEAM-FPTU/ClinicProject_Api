using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.ExaminationServices.RemoveService;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.RemoveService.HttpResoponseMapper;

/// <summary>
///     Mapper for  RemoveService feature
/// </summary>
public class RemoveServiceHttpResponseManager
{
    private readonly Dictionary<
        RemoveServiceResponseStatusCode,
        Func<RemoveServiceRequest, RemoveServiceResponse, RemoveServiceHttpResponse>
    > _dictionary;

    internal RemoveServiceHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveServiceResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveServiceResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveServiceResponseStatusCode.FORBIDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveServiceResponseStatusCode.SERVICE_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveServiceRequest,
        RemoveServiceResponse,
        RemoveServiceHttpResponse
    > Resolve(RemoveServiceResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}

