using System;
using System.Collections.Generic;
using Clinic.Application.Features.Schedules.RemoveAllSchedules;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Schedules.RemoveAllSchedules.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveAllSchedule feature
/// </summary>
public class RemoveAllSchedulesHttpResponseManager
{
    private readonly Dictionary<
        RemoveAllSchedulesResponseStatusCode,
        Func<RemoveAllSchedulesRequest, RemoveAllSchedulesResponse, RemoveAllSchedulesHttpResponse>
    > _dictionary;

    internal RemoveAllSchedulesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveAllSchedulesResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveAllSchedulesResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveAllSchedulesResponseStatusCode.FORBIDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

    }

    internal Func<
        RemoveAllSchedulesRequest,
        RemoveAllSchedulesResponse,
        RemoveAllSchedulesHttpResponse
    > Resolve(RemoveAllSchedulesResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
