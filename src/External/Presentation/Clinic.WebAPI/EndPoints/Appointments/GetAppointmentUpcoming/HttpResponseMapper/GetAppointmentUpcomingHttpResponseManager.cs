using System;
using System.Collections.Generic;
using Clinic.Application.Features.Appointments.GetAppointmentUpcoming;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Appointments.GetAppointmentUpcoming.HttpResponseMapper;

public class GetAppointmentUpcomingHttpResponseManager
{
    private readonly Dictionary<
        GetAppointmentUpcomingResponseStatusCode,
        Func<
            GetAppointmentUpcomingRequest,
            GetAppointmentUpcomingResponse,
            GetAppointmentUpcomingHttpResponse
        >
    > _dictionary;

    internal GetAppointmentUpcomingHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAppointmentUpcomingResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetAppointmentUpcomingResponseStatusCode.ROLE_IS_NOT_USER,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAppointmentUpcomingRequest,
        GetAppointmentUpcomingResponse,
        GetAppointmentUpcomingHttpResponse
    > Resolve(GetAppointmentUpcomingResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
