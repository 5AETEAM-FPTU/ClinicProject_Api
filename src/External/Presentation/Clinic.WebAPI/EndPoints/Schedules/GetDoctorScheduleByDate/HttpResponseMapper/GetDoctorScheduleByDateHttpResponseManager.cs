using System;
using System.Collections.Generic;
using Clinic.Application.Features.Schedules.GetDoctorMonthlyDate;
using Clinic.Application.Features.Schedules.GetDoctorScheduleByDate;
using Clinic.WebAPI.EndPoints.Schedules.GetDoctorMonthlyDate.HttpResponseMapper;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Schedules.GetDoctorScheduleByDate.HttpResponseMapper;

public class GetDoctorScheduleByDateHttpResponseManager
{
    private readonly Dictionary<
        GetDoctorScheduleByDateResponseStatusCode,
        Func<
            GetDoctorScheduleByDateRequest,
            GetDoctorScheduleByDateResponse,
            GetDoctorScheduleByDateHttpResponse
        >
    > _dictionary;

    internal GetDoctorScheduleByDateHttpResponseManager()
    {
        _dictionary = [];
        _dictionary.Add(
            key: GetDoctorScheduleByDateResponseStatusCode.DOCTOR_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: GetDoctorScheduleByDateResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );
        _dictionary.Add(
            key: GetDoctorScheduleByDateResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetDoctorScheduleByDateRequest,
        GetDoctorScheduleByDateResponse,
        GetDoctorScheduleByDateHttpResponse
    > Resolve(GetDoctorScheduleByDateResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
