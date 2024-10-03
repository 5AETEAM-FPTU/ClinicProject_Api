using System.Collections.Generic;
using System;
using Clinic.Application.Features.Users.GetRecentMedicalReport;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Users.GetRecentMedicalReport.HttpResponseMapper;

public class GetRecentMedicalReportHttpResponseManager
{
    private readonly Dictionary<
        GetRecentMedicalReportResponseStatusCode,
        Func<GetRecentMedicalReportRequest, GetRecentMedicalReportResponse, GetRecentMedicalReportHttpResponse>
    > _dictionary;

    internal GetRecentMedicalReportHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetRecentMedicalReportResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );



        _dictionary.Add(
            key: GetRecentMedicalReportResponseStatusCode.ROLE_IS_NOT_USER,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetRecentMedicalReportRequest,
        GetRecentMedicalReportResponse,
        GetRecentMedicalReportHttpResponse
    > Resolve(GetRecentMedicalReportResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}


