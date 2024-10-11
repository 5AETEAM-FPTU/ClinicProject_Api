using System;
using System.Collections.Generic;
using Clinic.Application.Features.Appointments.CreateNewAppointment;
using Clinic.Application.Features.OnlinePayments.CreateNewOnlinePayment;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Payments.CreateNewOnlinePayment.HttpResponseMapper;

public class CreateNewOnlinePaymentHttpResponseManager
{
    private readonly Dictionary<
        CreateNewOnlinePaymentResponseStatusCode,
        Func<
            CreateNewOnlinePaymentRequest,
            CreateNewOnlinePaymentResponse,
            CreateNewOnlinePaymentHttpResponse
        >
    > _dictionary;

    internal CreateNewOnlinePaymentHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateNewOnlinePaymentResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateNewOnlinePaymentResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateNewOnlinePaymentResponseStatusCode.UNAUTHORIZE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateNewOnlinePaymentResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateNewOnlinePaymentResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateNewOnlinePaymentResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateNewOnlinePaymentResponseStatusCode.APPOINTMENT_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
        _dictionary.Add(
            key: CreateNewOnlinePaymentResponseStatusCode.APPOINTMENT_HAS_DEPOSITED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status406NotAcceptable,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        CreateNewOnlinePaymentRequest,
        CreateNewOnlinePaymentResponse,
        CreateNewOnlinePaymentHttpResponse
    > Resolve(CreateNewOnlinePaymentResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
