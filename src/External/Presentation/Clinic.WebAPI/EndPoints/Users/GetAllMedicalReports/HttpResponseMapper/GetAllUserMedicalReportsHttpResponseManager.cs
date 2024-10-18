using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Users.GetAllMedicalReports;

namespace Clinic.WebAPI.EndPoints.Users.GetAllMedicalReports.HttpResponseMapper;

public class GetAllUserMedicalReportsHttpResponseManager
{
    private readonly Dictionary<
        GetAllUserMedicalReportsResponseStatusCode,
        Func<GetAllUserMedicalReportsRequest, GetAllUserMedicalReportsResponse, GetAllUserMedicalReportsHttpResponse>
    > _dictionary;

    internal GetAllUserMedicalReportsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllUserMedicalReportsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );


        _dictionary.Add(
            key: GetAllUserMedicalReportsResponseStatusCode.USER_NOT_FOUND,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetAllUserMedicalReportsResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

    }

    internal Func<
        GetAllUserMedicalReportsRequest,
        GetAllUserMedicalReportsResponse,
        GetAllUserMedicalReportsHttpResponse
    > Resolve(GetAllUserMedicalReportsResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
