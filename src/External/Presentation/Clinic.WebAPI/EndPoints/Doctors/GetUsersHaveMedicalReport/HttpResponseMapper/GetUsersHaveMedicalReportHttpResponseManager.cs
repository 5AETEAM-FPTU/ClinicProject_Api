using System;
using System.Collections.Generic;
using Clinic.Application.Features.Doctors.GetUsersHaveMedicalReport;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetUsersHaveMedicalReport.HttpResponseMapper;

public class GetUsersHaveMedicalReportHttpResponseManager
{
    private readonly Dictionary<
        GetUsersHaveMedicalReportResponseStatusCode,
        Func<
            GetUsersHaveMedicalReportRequest,
            GetUsersHaveMedicalReportResponse,
            GetUsersHaveMedicalReportHttpResponse
        >
    > _dictionary;

    internal GetUsersHaveMedicalReportHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetUsersHaveMedicalReportResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetUsersHaveMedicalReportResponseStatusCode.ROLE_IS_NOT_DOCTOR_STAFF,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetUsersHaveMedicalReportRequest,
        GetUsersHaveMedicalReportResponse,
        GetUsersHaveMedicalReportHttpResponse
    > Resolve(GetUsersHaveMedicalReportResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
