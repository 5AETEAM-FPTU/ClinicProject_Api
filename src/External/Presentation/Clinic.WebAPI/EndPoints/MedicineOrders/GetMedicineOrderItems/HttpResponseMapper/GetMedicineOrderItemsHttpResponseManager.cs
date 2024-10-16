using System;
using System.Collections.Generic;
using Clinic.Application.Features.MedicinnOrders.GetMedicineOrderItems;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicineOrders.GetMedicineOrderItems.HttpResponseMapper;

/// <summary>
///     Mapper for GetMedicineOrderItems feature
/// </summary>
public class GetMedicineOrderItemsHttpResponseManager
{
    private readonly Dictionary<
        GetMedicineOrderItemsResponseStatusCode,
        Func<GetMedicineOrderItemsRequest, GetMedicineOrderItemsResponse, GetMedicineOrderItemsHttpResponse>
    > _dictionary;

    internal GetMedicineOrderItemsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetMedicineOrderItemsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetMedicineOrderItemsResponseStatusCode.SERVICE_ORDER_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        GetMedicineOrderItemsRequest,
        GetMedicineOrderItemsResponse,
        GetMedicineOrderItemsHttpResponse
    > Resolve(GetMedicineOrderItemsResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
