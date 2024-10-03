using System.Collections.Generic;
using System;
using Clinic.Application.Features.Doctors.GetMedicalReportById;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetMedicalReportById.HttpResponseMapper;

public class GetMedicalReportByIdHttpResponseManager
{
    private readonly Dictionary<
        GetMedicalReportByIdResponseStatusCode,
        Func<GetMedicalReportByIdRequest, GetMedicalReportByIdResponse, GetMedicalReportByIdHttpResponse>
    > _dictionary;

    internal GetMedicalReportByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetMedicalReportByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetMedicalReportByIdResponseStatusCode.REPORT_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        GetMedicalReportByIdRequest,
        GetMedicalReportByIdResponse,
        GetMedicalReportByIdHttpResponse
    > Resolve(GetMedicalReportByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
