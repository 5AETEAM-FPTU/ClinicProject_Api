using System;
using System.Collections.Generic;
using Clinic.Application.Features.Feedbacks.SendFeedBack;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Feedbacks.SendFeedBack.HttpResponseMapper;

/// <summary>
///     Mapper for SendFeedBack feature
/// </summary>
public class SendFeedBackHttpResponseManager
{
    private readonly Dictionary<
        SendFeedBackResponseStatusCode,
        Func<SendFeedBackRequest, SendFeedBackResponse, SendFeedBackHttpResponse>
    > _dictionary;

    internal SendFeedBackHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: SendFeedBackResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: SendFeedBackResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: SendFeedBackResponseStatusCode.FORBIDEN_ACCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: SendFeedBackResponseStatusCode.FEEDBACK_IS_ALREADY_SENT,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
           key: SendFeedBackResponseStatusCode.APPOINTMENT_IS_NOT_COMPLETED,
           value: (_, response) =>
               new()
               {
                   HttpCode = StatusCodes.Status400BadRequest,
                   AppCode = response.StatusCode.ToAppCode(),
               }
       );
    }

    internal Func<SendFeedBackRequest, SendFeedBackResponse, SendFeedBackHttpResponse> Resolve(
        SendFeedBackResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
