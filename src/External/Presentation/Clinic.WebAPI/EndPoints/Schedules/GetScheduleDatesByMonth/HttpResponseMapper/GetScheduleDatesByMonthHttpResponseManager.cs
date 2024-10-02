using System;
using System.Collections.Generic;
using Clinic.Application.Features.Schedules.GetScheduleDatesByMonth;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Schedules.GetScheduleDatesByMonth.HttpResponseMapper;

/// <summary>
///     Mapper for GetSchedulesByDate feature
/// </summary>
public class GetScheduleDatesByMonthHttpResponseManager
{
    private readonly Dictionary<
        GetScheduleDatesByMonthResponseStatusCode,
        Func<GetScheduleDatesByMonthRequest, GetScheduleDatesByMonthResponse, GetScheduleDatesByMonthHttpResponse>
    > _dictionary;

    internal GetScheduleDatesByMonthHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetScheduleDatesByMonthResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetScheduleDatesByMonthResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

    }

    internal Func<
        GetScheduleDatesByMonthRequest,
        GetScheduleDatesByMonthResponse,
        GetScheduleDatesByMonthHttpResponse
    > Resolve(GetScheduleDatesByMonthResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
