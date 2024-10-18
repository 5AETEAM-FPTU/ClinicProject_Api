using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.ExaminationServices.UpdateService;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.UpdateService.HttpResoponseMapper;

/// <summary>
///     Mapper for  UpdateService feature
/// </summary>
public class UpdateServiceHttpResponseManager
{
    private readonly Dictionary<
        UpdateServiceResponseStatusCode,
        Func<UpdateServiceRequest, UpdateServiceResponse, UpdateServiceHttpResponse>
    > _dictionary;

    internal UpdateServiceHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateServiceResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateServiceResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateServiceResponseStatusCode.ROLE_IS_NOT_ADMIN_STAFF,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateServiceResponseStatusCode.SERVICE_CODE_ALREADY_EXISTED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateServiceResponseStatusCode.SERVICE_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdateServiceRequest,
        UpdateServiceResponse,
        UpdateServiceHttpResponse
    > Resolve(UpdateServiceResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}

