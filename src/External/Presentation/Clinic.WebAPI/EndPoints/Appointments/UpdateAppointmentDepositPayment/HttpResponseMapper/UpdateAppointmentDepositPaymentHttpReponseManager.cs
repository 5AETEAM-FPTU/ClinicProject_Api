using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;

public class UpdateAppointmentDepositPaymentHttpReponseManager {

    private readonly Dictionary<UpdateAppointmentDepositPaymentResponseStatusCode, Func<UpdateAppoinmentDepositPaymenRequest, UpdateAppoinmentDepositPaymentResponse, UpdateAppointmentDepositPaymentHttpResponse>> _dictionary;

    internal UpdateAppointmentDepositPaymentHttpReponseManager() {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateAppointmentDepositPaymentResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateAppointmentDepositPaymentResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateAppointmentDepositPaymentResponseStatusCode.UNAUTHORIZE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateAppointmentDepositPaymentResponseStatusCode.APPOINTMENTS_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateAppointmentDepositPaymentResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateAppointmentDepositPaymentResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: UpdateAppointmentDepositPaymentResponseStatusCode.ROLE_IS_NOT_ACCEPTABLE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status406NotAcceptable,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        
    }
    internal Func<
        UpdateAppoinmentDepositPaymenRequest,
        UpdateAppoinmentDepositPaymentResponse,
        UpdateAppointmentDepositPaymentHttpResponse
    > Resolve(UpdateAppointmentDepositPaymentResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}