using System.Collections.Generic;
using System;
using Clinic.Application.Features.Users.GetConsultationOverview;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Users.GetConsultationOverview.HttpResponseMapper;

public class GetConsultationOverviewHttpResponseManager
{
    private readonly Dictionary<
        GetConsultationOverviewResponseStatusCode,
        Func<GetConsultationOverviewRequest, GetConsultationOverviewResponse, GetConsultationOverviewHttpResponse>
    > _dictionary;

    internal GetConsultationOverviewHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetConsultationOverviewResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );



        _dictionary.Add(
            key: GetConsultationOverviewResponseStatusCode.ROLE_IS_NOT_USER,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetConsultationOverviewRequest,
        GetConsultationOverviewResponse,
        GetConsultationOverviewHttpResponse
    > Resolve(GetConsultationOverviewResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
