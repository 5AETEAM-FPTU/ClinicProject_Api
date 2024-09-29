using System;
using System.Collections.Generic;
using Clinic.Application.Features.Schedules.CreateSchedules;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Schedules.CreateSchedules.HttpResponseMapper;

/// <summary>
///     Mapper for CreateSchedules feature
/// </summary>
public class CreateSchedulesHttpResponseManager
{
    private readonly Dictionary<
        CreateSchedulesResponseStatusCode,
        Func<CreateSchedulesRequest, CreateSchedulesResponse, CreateSchedulesHttpResponse>
    > _dictionary;

    internal CreateSchedulesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateSchedulesResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateSchedulesResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateSchedulesResponseStatusCode.FORBIDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateSchedulesResponseStatusCode.TIMESLOT_IS_EXIST,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        CreateSchedulesRequest,
        CreateSchedulesResponse,
        CreateSchedulesHttpResponse
    > Resolve(CreateSchedulesResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
