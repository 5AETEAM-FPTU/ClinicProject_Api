using Clinic.Application.Features.Appointments.UpdateUserBookedAppointment;
using Clinic.Application.Features.Appointments.UpdateUserBookedAppointment;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Appointments.UpdateUserBookedAppointment.HttpResponseMapper;

public class UpdateUserBookedAppointmentHttpResponseManager
{
    private readonly Dictionary<UpdateUserBookedAppointmentResponseStatusCode, Func<UpdateUserBookedAppointmentRequest, UpdateUserBookedAppointmentResponse, UpdateUserBookedAppointmentHttpResponse>> _dictionary;

    internal UpdateUserBookedAppointmentHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateUserBookedAppointmentResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateUserBookedAppointmentResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateUserBookedAppointmentResponseStatusCode.UNAUTHORIZE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateUserBookedAppointmentResponseStatusCode.APPOINTMENTS_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateUserBookedAppointmentResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateUserBookedAppointmentResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateUserBookedAppointmentResponseStatusCode.ROLE_IS_NOT_ACCEPTABLE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status406NotAcceptable,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateUserBookedAppointmentResponseStatusCode.APPOINTMENT_ONLY_UPDATE_ONCE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status406NotAcceptable,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateUserBookedAppointmentResponseStatusCode.UPDATE_EXPIRED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status406NotAcceptable,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }
    internal Func<
        UpdateUserBookedAppointmentRequest,
        UpdateUserBookedAppointmentResponse,
        UpdateUserBookedAppointmentHttpResponse
    > Resolve(UpdateUserBookedAppointmentResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
