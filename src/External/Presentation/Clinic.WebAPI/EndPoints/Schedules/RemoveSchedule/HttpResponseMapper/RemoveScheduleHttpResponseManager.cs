using System;
using System.Collections.Generic;
using Clinic.Application.Features.Schedules.RemoveSchedule;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Schedules.RemoveSchedule.HttpResponseMapper;

/// <summary>
///     Mapper for UpdateSchedule feature
/// </summary>
public class RemoveScheduleHttpResponseManager
{
    private readonly Dictionary<
        RemoveScheduleResponseStatusCode,
        Func<RemoveScheduleRequest, RemoveScheduleResponse, RemoveScheduleHttpResponse>
    > _dictionary;

    internal RemoveScheduleHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveScheduleResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveScheduleResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveScheduleResponseStatusCode.FORBIDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveScheduleResponseStatusCode.SCHEDULE_HAD_APPOINTMENT,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveScheduleResponseStatusCode.NOT_FOUND_SCHEDULE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveScheduleRequest,
        RemoveScheduleResponse,
        RemoveScheduleHttpResponse
    > Resolve(RemoveScheduleResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
