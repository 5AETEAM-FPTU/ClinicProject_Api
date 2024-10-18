using System;
using System.Collections.Generic;
using Clinic.Application.Features.ServiceOrders.AddOrderService;
using Clinic.Application.Features.ServiceOrders.GetServiceOrderItems;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.ServiceOrders.GetServiceOrderItems.HttpResponseMapper;

/// <summary>
///     Mapper for GetServiceOrderItems feature
/// </summary>
public class GetServiceOrderItemsHttpResponseManager
{
    private readonly Dictionary<
        GetServiceOrderItemsResponseStatusCode,
        Func<GetServiceOrderItemsRequest, GetServiceOrderItemsResponse, GetServiceOrderItemsHttpResponse>
    > _dictionary;

    internal GetServiceOrderItemsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetServiceOrderItemsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetServiceOrderItemsResponseStatusCode.SERVICE_ORDER_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        GetServiceOrderItemsRequest,
        GetServiceOrderItemsResponse,
        GetServiceOrderItemsHttpResponse
    > Resolve(GetServiceOrderItemsResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
