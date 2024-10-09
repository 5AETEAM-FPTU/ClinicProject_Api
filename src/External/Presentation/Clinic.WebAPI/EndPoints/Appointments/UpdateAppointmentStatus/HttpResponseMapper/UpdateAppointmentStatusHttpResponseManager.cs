using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentStatus;

public class UpdateAppointmentStatusHttpResponseManager
{
    private readonly Dictionary<
        UpdateAppointmentStatusResponseStatusCode,
        Func<
            UpdateAppointmentStatusRequest,
            UpdateAppointmentStatusResponse,
            UpdateAppointmentStatusHttpResponse
        >
    > _dictionary;

    internal UpdateAppointmentStatusHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateAppointmentStatusResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateAppointmentStatusResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateAppointmentStatusResponseStatusCode.APPOINTMENT_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateAppointmentStatusResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateAppointmentStatusResponseStatusCode.UNAUTHORIZE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateAppointmentStatusResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateAppointmentStatusResponseStatusCode.STATUS_IS_NOT_ACCEPTABLE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status406NotAcceptable,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdateAppointmentStatusRequest,
        UpdateAppointmentStatusResponse,
        UpdateAppointmentStatusHttpResponse
    > Resolve(UpdateAppointmentStatusResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
