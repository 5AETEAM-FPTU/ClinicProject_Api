using System;
using System.Collections.Generic;
using Clinic.Application.Features.Doctors.GetUserNotification;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Doctors.GetUserNotification.HttpResponseMapper;

public class GetUserNotificationHttpResponseManager
{
    private readonly Dictionary<
        GetUserNotificationResponseStatusCode,
        Func<
            GetUserNotificationRequest,
            GetUserNotificationResponse,
            GetUserNotificationHttpResponse
        >
    > _dictionary;

    internal GetUserNotificationHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetUserNotificationResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetUserNotificationResponseStatusCode.USER_ID_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetUserNotificationResponseStatusCode.ROLE_IS_NOT_DOCTOR_STAFF,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetUserNotificationRequest,
        GetUserNotificationResponse,
        GetUserNotificationHttpResponse
    > Resolve(GetUserNotificationResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
