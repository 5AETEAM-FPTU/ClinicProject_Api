using System;
using System.Collections.Generic;
using Clinic.Application.Features.Users.GetUserMedicalReport;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Users.GetUserMedicalReport.HttpResponseMapper;

public class GetUserMedicalReportHttpResponseManager
{
    private readonly Dictionary<
        GetUserMedicalReportResponseStatusCode,
        Func<
            GetUserMedicalReportRequest,
            GetUserMedicalReportResponse,
            GetUserMedicalReportHttpResponse
        >
    > _dictionary;

    internal GetUserMedicalReportHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetUserMedicalReportResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetUserMedicalReportResponseStatusCode.MEDICAL_REPORT_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetUserMedicalReportResponseStatusCode.ROLE_IS_NOT_USER,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetUserMedicalReportRequest,
        GetUserMedicalReportResponse,
        GetUserMedicalReportHttpResponse
    > Resolve(GetUserMedicalReportResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
