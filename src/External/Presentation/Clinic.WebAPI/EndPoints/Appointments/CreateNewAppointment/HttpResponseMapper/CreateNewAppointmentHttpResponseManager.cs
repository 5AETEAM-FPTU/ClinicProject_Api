using System;
using System.Collections.Generic;
using Clinic.Application.Features.Appointments.CreateNewAppointment;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Appointments.CreateNewAppointment.HttpResponseMapper;

public class CreateNewAppointmentHttpResponseManager
{
    private readonly Dictionary<
        CreateNewAppointmentResponseStatusCode,
        Func<
            CreateNewAppointmentRequest,
            CreateNewAppointmentResponse,
            CreateNewAppointmentHttpResponse
        >
    > _dictionary;

    internal CreateNewAppointmentHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateNewAppointmentResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
        _dictionary.Add(
            key: CreateNewAppointmentResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateNewAppointmentResponseStatusCode.UNAUTHORIZE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateNewAppointmentResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateNewAppointmentResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateNewAppointmentResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateNewAppointmentResponseStatusCode.SCHEDUELE_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateNewAppointmentResponseStatusCode.SCHEDUELE_IS_NOT_AVAILABLE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status405MethodNotAllowed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        CreateNewAppointmentRequest,
        CreateNewAppointmentResponse,
        CreateNewAppointmentHttpResponse
    > Resolve(CreateNewAppointmentResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
