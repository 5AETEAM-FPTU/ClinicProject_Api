using System;
using System.Collections.Generic;
using Clinic.Application.Features.Appointments.GetRecentAbsent;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Appointments.GetRecentAbsent.HttpResponseMapper;

public class GetRecentAbsentHttpResponseManager
{
    private readonly Dictionary<
        GetRecentAbsentResponseStatusCode,
        Func<GetRecentAbsentRequest, GetRecentAbsentResponse, GetRecentAbsentHttpResponse>
    > _dictionary;

    internal GetRecentAbsentHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetRecentAbsentResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetRecentAbsentResponseStatusCode.ROLE_IS_NOT_STAFF,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetRecentAbsentRequest,
        GetRecentAbsentResponse,
        GetRecentAbsentHttpResponse
    > Resolve(GetRecentAbsentResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
