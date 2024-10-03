using System;
using System.Collections.Generic;
using Clinic.Application.Features.Appointments.GetAbsentAppointment;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Appointments.GetAbsentAppointment.HttpResponseMapper;

public class GetAbsentAppointmentHttpResponseManager
{
    private readonly Dictionary<
        GetAbsentAppointmentResponseStatusCode,
        Func<
            GetAbsentAppointmentRequest,
            GetAbsentAppointmentResponse,
            GetAbsentAppointmentHttpResponse
        >
    > _dictionary;

    internal GetAbsentAppointmentHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAbsentAppointmentResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetAbsentAppointmentResponseStatusCode.ROLE_IS_NOT_DOCTOR,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAbsentAppointmentRequest,
        GetAbsentAppointmentResponse,
        GetAbsentAppointmentHttpResponse
    > Resolve(GetAbsentAppointmentResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
