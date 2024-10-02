using System.Collections.Generic;
using System;
using Clinic.Application.Features.Doctors.GetAllMedicalReport;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAllMedicalReport.HttpResponseMapper;

public class GetAllMedicalReportHttpResponseManager
{
    private readonly Dictionary<
        GetAllMedicalReportResponseStatusCode,
        Func<GetAllMedicalReportRequest, GetAllMedicalReportResponse, GetAllMedicalReportHttpResponse>
    > _dictionary;

    internal GetAllMedicalReportHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllMedicalReportResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );



        _dictionary.Add(
            key: GetAllMedicalReportResponseStatusCode.ROLE_IS_NOT_DOCTOR_STAFF,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAllMedicalReportRequest,
        GetAllMedicalReportResponse,
        GetAllMedicalReportHttpResponse
    > Resolve(GetAllMedicalReportResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
