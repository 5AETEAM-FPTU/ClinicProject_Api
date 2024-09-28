using System.Collections.Generic;
using System;
using Clinic.Application.Features.Enums.GetAllPosition;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllPosition.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllPosition feature
/// </summary>
public class GetAllPositionHttpResponseManager
{
    private readonly Dictionary<
        GetAllPositionResponseStatusCode,
        Func<GetAllPositionRequest, GetAllPositionResponse, GetAllPositionHttpResponse>
    > _dictionary;

    internal GetAllPositionHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllPositionResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

    }

    internal Func<GetAllPositionRequest, GetAllPositionResponse, GetAllPositionHttpResponse> Resolve(
        GetAllPositionResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
