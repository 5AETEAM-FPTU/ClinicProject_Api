using System;
using System.Collections.Generic;
using Clinic.Application.Features.Feedbacks.DoctorGetAllFeedbacks;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Feedbacks.DoctorGetAllFeedbacks.HttpResponseMapper;

/// <summary>
///     Mapper for DoctorGetAllFeedback feature
/// </summary>
public class DoctorGetAllFeedBackHttpResponseManager
{
    private readonly Dictionary<
        DoctorGetAllFeedBackResponseStatusCode,
        Func<DoctorGetAllFeedBackRequest, DoctorGetAllFeedBackResponse, DoctorGetAllFeedBackHttpResponse>
    > _dictionary;

    internal DoctorGetAllFeedBackHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: DoctorGetAllFeedBackResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: DoctorGetAllFeedBackResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

    }

    internal Func<DoctorGetAllFeedBackRequest, DoctorGetAllFeedBackResponse, DoctorGetAllFeedBackHttpResponse> Resolve(
        DoctorGetAllFeedBackResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
