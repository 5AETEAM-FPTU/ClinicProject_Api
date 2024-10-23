using System;
using System.Collections.Generic;
using Clinic.Application.Features.ServiceOrders.UpdateStatusItem;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ServiceOrders.UpdateStatusItem.HttpResponseMapper;

/// <summary>
///     Mapper for GetServiceOrderItems feature
/// </summary>
public class UpdateStatusServiceOrderItemsHttpResponseManager
{
    private readonly Dictionary<
        UpdateStatusServiceOrderItemsResponseStatusCode,
        Func<UpdateStatusServiceOrderItemsRequest, UpdateStatusServiceOrderItemsResponse, UpdateStatusServiceOrderItemsHttpResponse>
    > _dictionary;

    internal UpdateStatusServiceOrderItemsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateStatusServiceOrderItemsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateStatusServiceOrderItemsResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateStatusServiceOrderItemsResponseStatusCode.SERVICE_ITEM_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdateStatusServiceOrderItemsResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

    }

    internal Func<
        UpdateStatusServiceOrderItemsRequest,
        UpdateStatusServiceOrderItemsResponse,
        UpdateStatusServiceOrderItemsHttpResponse
    > Resolve(UpdateStatusServiceOrderItemsResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
