using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.ExaminationServices.CreateService;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.CreateService.HttpResoponseMapper;

/// <summary>
///     Mapper for CreateMedicine feature
/// </summary>
public class CreateServiceHttpResponseManager
{
    private readonly Dictionary<
        CreateServiceResponseStatusCode,
        Func<CreateServiceRequest, CreateServiceResponse, CreateServiceHttpResponse>
    > _dictionary;

    internal CreateServiceHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateServiceResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateServiceResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateServiceResponseStatusCode.ROLE_IS_NOT_ADMIN_STAFF,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateServiceResponseStatusCode.SERVICE_CODE_ALREADY_EXISTED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        CreateServiceRequest,
        CreateServiceResponse,
        CreateServiceHttpResponse
    > Resolve(CreateServiceResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}

