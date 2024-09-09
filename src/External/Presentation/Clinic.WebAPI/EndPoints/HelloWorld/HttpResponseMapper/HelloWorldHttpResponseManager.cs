using System;
using System.Collections.Generic;
using Clinic.Application.Features.HelloWorld;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.HelloWorld.HttpResponseMapper;

/// <summary>
///     Mapper for HelloWorld feature
/// </summary>
public class HelloWorldHttpResponseManager
{
    private readonly Dictionary<
        HelloWorldResponseStatusCode,
        Func<HelloWorldRequest, HelloWorldResponse, HelloWorldHttpResponse>
    > _dictionary;

    internal HelloWorldHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: HelloWorldResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: HelloWorldResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<HelloWorldRequest, HelloWorldResponse, HelloWorldHttpResponse> Resolve(
        HelloWorldResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
