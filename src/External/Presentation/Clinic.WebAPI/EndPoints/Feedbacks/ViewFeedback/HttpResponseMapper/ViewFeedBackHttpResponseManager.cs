using System;
using System.Collections.Generic;
using Clinic.Application.Features.Feedbacks.ViewFeedback;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Feedbacks.ViewFeedback.HttpResponseMapper;

/// <summary>
///     Mapper for ViewFeedback feature
/// </summary>
public class ViewFeedBackHttpResponseManager
{
    private readonly Dictionary<
        ViewFeedBackResponseStatusCode,
        Func<ViewFeedBackRequest, ViewFeedBackResponse, ViewFeedBackHttpResponse>
    > _dictionary;

    internal ViewFeedBackHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: ViewFeedBackResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: ViewFeedBackResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ViewFeedBackResponseStatusCode.FEEDBACK_HAVE_NOT_SENT,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ViewFeedBackResponseStatusCode.APPOINTMENT_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

    }

    internal Func<ViewFeedBackRequest, ViewFeedBackResponse, ViewFeedBackHttpResponse> Resolve(
        ViewFeedBackResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
