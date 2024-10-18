using System.Collections.Generic;
using System;
using Clinic.Application.Features.Doctors.GetRecentMedicalReportByUserId;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetRecentMedicalReportByUserId.HttpResponseMapper;

/// <summary>
///     Mapper for GetRecentMedicalReportByUserId feature
/// </summary>
public class GetRecentMedicalReportByUserIdHttpResponseManager
{
    private readonly Dictionary<
        GetRecentMedicalReportByUserIdResponseStatusCode,
        Func<GetRecentMedicalReportByUserIdRequest, GetRecentMedicalReportByUserIdResponse, GetRecentMedicalReportByUserIdHttpResponse>
    > _dictionary;

    internal GetRecentMedicalReportByUserIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetRecentMedicalReportByUserIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetRecentMedicalReportByUserIdResponseStatusCode.USER_ID_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
           key: GetRecentMedicalReportByUserIdResponseStatusCode.ROLE_IS_NOT_DOCTOR_STAFF,
           value: (_, response) =>
               new()
               {
                   HttpCode = StatusCodes.Status403Forbidden,
                   AppCode = response.StatusCode.ToAppCode()
               }
       );
    }

    internal Func<
        GetRecentMedicalReportByUserIdRequest,
        GetRecentMedicalReportByUserIdResponse,
        GetRecentMedicalReportByUserIdHttpResponse
    > Resolve(GetRecentMedicalReportByUserIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
