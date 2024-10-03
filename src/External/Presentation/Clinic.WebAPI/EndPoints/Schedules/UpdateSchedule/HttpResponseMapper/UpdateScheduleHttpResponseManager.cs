using System;
using System.Collections.Generic;
using Clinic.Application.Features.Schedules.UpdateSchedule;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Schedules.UpdateSchedule.HttpResponseMapper;

/// <summary>
///     Mapper for UpdateSchedule feature
/// </summary>
public class UpdateScheduleHttpResponseManager
{
    private readonly Dictionary<
        UpdateScheduleResponseStatusCode,
        Func<UpdateScheduleRequest, UpdateScheduleResponse, UpdateScheduleHttpResponse>
    > _dictionary;

    internal UpdateScheduleHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateScheduleResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateScheduleResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateScheduleResponseStatusCode.FORBIDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateScheduleResponseStatusCode.SCHEDULE_WAS_OVERLAPED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateScheduleResponseStatusCode.NOT_FOUND_SCHEDULE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdateScheduleRequest,
        UpdateScheduleResponse,
        UpdateScheduleHttpResponse
    > Resolve(UpdateScheduleResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
