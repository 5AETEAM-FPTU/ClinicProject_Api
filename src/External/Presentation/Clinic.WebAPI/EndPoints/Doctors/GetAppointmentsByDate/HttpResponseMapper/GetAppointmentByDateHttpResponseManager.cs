using System;
using System.Collections.Generic;
using Clinic.Application.Features.Doctors.GetAppointmentsByDate;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAppointmentsByDate.HttpResponseMapper;

/// <summary>
///     Mapper for GetSchedulesByDate feature
/// </summary>
public class GetAppointmentByDateHttpResponseManager
{
    private readonly Dictionary<
        GetAppointmentsByDateResponseStatusCode,
        Func<GetAppointmentsByDateRequest, GetAppointmentsByDateResponse, GetAppointmentsByDateHttpResponse>
    > _dictionary;

    internal GetAppointmentByDateHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAppointmentsByDateResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetAppointmentsByDateResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        GetAppointmentsByDateRequest,
        GetAppointmentsByDateResponse,
        GetAppointmentsByDateHttpResponse
    > Resolve(GetAppointmentsByDateResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
