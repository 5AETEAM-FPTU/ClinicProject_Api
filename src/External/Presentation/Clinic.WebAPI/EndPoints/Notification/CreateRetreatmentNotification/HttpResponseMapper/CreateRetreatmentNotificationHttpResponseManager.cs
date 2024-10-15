using System;
using System.Collections.Generic;
using Clinic.Application.Features.Notification.CreateRetreatmentNotification;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Notification.CreateRetreatmentNotification.HttpResponseMapper;

/// <summary>
///     Mapper for CreateRetreatmentNotification feature
/// </summary>
public class CreateRetreatmentNotificationHttpResponseManager
{
    private readonly Dictionary<
        CreateRetreatmentNotificationResponseStatusCode,
        Func<
            CreateRetreatmentNotificationRequest,
            CreateRetreatmentNotificationResponse,
            CreateRetreatmentNotificationHttpResponse
        >
    > _dictionary;

    internal CreateRetreatmentNotificationHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateRetreatmentNotificationResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateRetreatmentNotificationResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateRetreatmentNotificationResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateRetreatmentNotificationResponseStatusCode.NOTIFICATION_ALREADY_EXISTED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
           key: CreateRetreatmentNotificationResponseStatusCode.SMS_NOTIFICATION_FAIL,
           value: (_, response) =>
               new()
               {
                   HttpCode = StatusCodes.Status417ExpectationFailed,
                   AppCode = response.StatusCode.ToAppCode(),
               }
       );
    }

    internal Func<
        CreateRetreatmentNotificationRequest,
        CreateRetreatmentNotificationResponse,
        CreateRetreatmentNotificationHttpResponse
    > Resolve(CreateRetreatmentNotificationResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
