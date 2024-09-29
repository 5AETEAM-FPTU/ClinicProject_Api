using System;
using System.Collections.Generic;
using Clinic.Application.Features.Schedules.GetSchedulesByDate;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Schedules.GetSchedulesByDate.HttpResponseMapper;

/// <summary>
///     Mapper for GetSchedulesByDate feature
/// </summary>
public class GetSchedulesByDateHttpResponseManager
{
    private readonly Dictionary<
        GetSchedulesByDateResponseStatusCode,
        Func<GetSchedulesByDateRequest, GetSchedulesByDateResponse, GetSchedulesByDateHttpResponse>
    > _dictionary;

    internal GetSchedulesByDateHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetSchedulesByDateResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );
    }

    internal Func<
        GetSchedulesByDateRequest,
        GetSchedulesByDateResponse,
        GetSchedulesByDateHttpResponse
    > Resolve(GetSchedulesByDateResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
