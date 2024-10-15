using System;
using System.Collections.Generic;
using Clinic.Application.Features.ServiceOrders.AddOrderService;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ServiceOrders.AddOrderService.HttpResponseMapper;

/// <summary>
///     Mapper for AddOrderService feature
/// </summary>
public class AddOrderServiceHttpResponseManager
{
    private readonly Dictionary<
        AddOrderServiceResponseStatusCode,
        Func<AddOrderServiceRequest, AddOrderServiceResponse, AddOrderServiceHttpResponse>
    > _dictionary;

    internal AddOrderServiceHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: AddOrderServiceResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: AddOrderServiceResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: AddOrderServiceResponseStatusCode.SERVICE_ORDER_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: AddOrderServiceResponseStatusCode.SERVICE_NOT_AVAILABLE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: AddOrderServiceResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        AddOrderServiceRequest,
        AddOrderServiceResponse,
        AddOrderServiceHttpResponse
    > Resolve(AddOrderServiceResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
