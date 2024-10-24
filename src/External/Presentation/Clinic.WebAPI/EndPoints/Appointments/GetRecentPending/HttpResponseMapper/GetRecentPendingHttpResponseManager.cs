using System;
using System.Collections.Generic;
using Clinic.Application.Features.Appointments.GetRecentPending;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Appointments.GetRecentPending.HttpResponseMapper;

public class GetRecentPendingHttpResponseManager
{
    private readonly Dictionary<
        GetRecentPendingResponseStatusCode,
        Func<GetRecentPendingRequest, GetRecentPendingResponse, GetRecentPendingHttpResponse>
    > _dictionary;

    internal GetRecentPendingHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetRecentPendingResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetRecentPendingResponseStatusCode.ROLE_IS_NOT_STAFF,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetRecentPendingRequest,
        GetRecentPendingResponse,
        GetRecentPendingHttpResponse
    > Resolve(GetRecentPendingResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
