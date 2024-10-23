using System;
using System.Collections.Generic;
using Clinic.Application.Features.Admin.RemovedDoctorTemporarily;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.RemovedDoctorTemporarily.HttpResponseMapper;

/// <summary>
///     Mapper for RemovedDoctorTemporarily feature
/// </summary>
public class RemovedDoctorTemporarilyHttpResponseManager
{
    private readonly Dictionary<
        RemovedDoctorTemporarilyResponseStatusCode,
        Func<
            RemovedDoctorTemporarilyRequest,
            RemovedDoctorTemporarilyResponse,
            RemovedDoctorTemporarilyHttpResponse
        >
    > _dictionary;

    internal RemovedDoctorTemporarilyHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemovedDoctorTemporarilyResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemovedDoctorTemporarilyResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemovedDoctorTemporarilyResponseStatusCode.FORBIDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemovedDoctorTemporarilyResponseStatusCode.DOCTOR_NOT_EXIST,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemovedDoctorTemporarilyRequest,
        RemovedDoctorTemporarilyResponse,
        RemovedDoctorTemporarilyHttpResponse
    > Resolve(RemovedDoctorTemporarilyResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
