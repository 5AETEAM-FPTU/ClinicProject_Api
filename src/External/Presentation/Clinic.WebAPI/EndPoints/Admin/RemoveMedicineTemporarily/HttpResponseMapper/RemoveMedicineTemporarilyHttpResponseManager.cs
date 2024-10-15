using System;
using System.Collections.Generic;
using Clinic.Application.Features.Admin.RemoveMedicineTemporarily;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.Admin.RemoveMedicineTemporarily.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveMedicineTemporarily feature
/// </summary>
public class RemoveMedicineTemporarilyHttpResponseManager
{
    private readonly Dictionary<
        RemoveMedicineTemporarilyResponseStatusCode,
        Func<
            RemoveMedicineTemporarilyRequest,
            RemoveMedicineTemporarilyResponse,
            RemoveMedicineTemporarilyHttpResponse
        >
    > _dictionary;

    internal RemoveMedicineTemporarilyHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveMedicineTemporarilyResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveMedicineTemporarilyResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveMedicineTemporarilyResponseStatusCode.FORBIDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveMedicineTemporarilyResponseStatusCode.NOT_FOUND_MEDICINE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveMedicineTemporarilyRequest,
        RemoveMedicineTemporarilyResponse,
        RemoveMedicineTemporarilyHttpResponse
    > Resolve(RemoveMedicineTemporarilyResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
