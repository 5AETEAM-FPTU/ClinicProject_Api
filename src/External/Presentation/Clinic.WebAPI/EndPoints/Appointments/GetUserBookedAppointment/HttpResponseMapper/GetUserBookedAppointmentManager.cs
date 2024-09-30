using System.Collections.Generic;
using System;
using Clinic.Application.Features.Appointments.GetUserBookedAppointment;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Appointments.GetUserBookedAppointment.HttpResponseMapper;

public class GetUserBookedAppointmentHttpResponseManager
{
    private readonly Dictionary<
        GetUserBookedAppointmentResponseStatusCode,
        Func<GetUserBookedAppointmentRequest, GetUserBookedAppointmentResponse, GetUserBookedAppointmentHttpResponse>
    > _dictionary;

    internal GetUserBookedAppointmentHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetUserBookedAppointmentResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetUserBookedAppointmentResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: GetUserBookedAppointmentResponseStatusCode.APPOINTMENTS_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

       
        _dictionary.Add(
            key: GetUserBookedAppointmentResponseStatusCode.ROLE_IS_NOT_USER,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetUserBookedAppointmentRequest,
        GetUserBookedAppointmentResponse,
        GetUserBookedAppointmentHttpResponse
    > Resolve(GetUserBookedAppointmentResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}

