using System;
using System.Collections.Generic;
using Clinic.Application.Features.Schedules.GetDoctorMonthlyDate;
using Clinic.Application.Features.Schedules.GetScheduleDatesByMonth;
using Clinic.WebAPI.EndPoints.Schedules.GetScheduleDatesByMonth.HttpResponseMapper;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Schedules.GetDoctorMonthlyDate.HttpResponseMapper;

public class GetDoctorMonthlyDateHttpResponseManager
{
    private readonly Dictionary<
        GetDoctorMonthlyDateResponseStatusCode,
        Func<
            GetDoctorMonthlyDateRequest,
            GetDoctorMonthlyDateResponse,
            GetDoctorMonthlyDateHttpResponse
        >
    > _dictionary;

    internal GetDoctorMonthlyDateHttpResponseManager()
    {
        _dictionary = [];
        _dictionary.Add(
            key: GetDoctorMonthlyDateResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );
        _dictionary.Add(
            key: GetDoctorMonthlyDateResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: GetDoctorMonthlyDateResponseStatusCode.DOCTOR_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetDoctorMonthlyDateRequest,
        GetDoctorMonthlyDateResponse,
        GetDoctorMonthlyDateHttpResponse
    > Resolve(GetDoctorMonthlyDateResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
