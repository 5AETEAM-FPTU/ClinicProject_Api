using System;
using System.Collections.Generic;
using Clinic.Application.Features.MedicalReports.GetMedicalReportsForStaff;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicalReports.GetMedicalReportsForStaff.HttpResponseMapper;

public class GetMedicalReportsForStaffHttpResponseManager
{
    private readonly Dictionary<
        GetMedicalReportsForStaffResponseStatusCode,
        Func<
            GetMedicalReportsForStaffRequest,
            GetMedicalReportsForStaffResponse,
            GetMedicalReportsForStaffHttpResponse
        >
    > _dictionary;

    internal GetMedicalReportsForStaffHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetMedicalReportsForStaffResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetMedicalReportsForStaffResponseStatusCode.ROLE_IS_NOT_DOCTOR_STAFF,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetMedicalReportsForStaffRequest,
        GetMedicalReportsForStaffResponse,
        GetMedicalReportsForStaffHttpResponse
    > Resolve(GetMedicalReportsForStaffResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
