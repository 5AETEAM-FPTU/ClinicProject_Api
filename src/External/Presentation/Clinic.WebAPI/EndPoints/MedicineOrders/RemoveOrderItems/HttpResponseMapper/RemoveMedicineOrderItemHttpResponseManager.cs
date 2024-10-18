using System;
using System.Collections.Generic;
using Clinic.Application.Features.MedicineOrders.RemoveOrderItems;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicineOrders.RemoveOrderItems.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveMedicineOrderItem feature
/// </summary>
public class RemoveMedicineOrderItemHttpResponseManager
{
    private readonly Dictionary<
        RemoveMedicineOrderItemResponseStatusCode,
        Func<RemoveMedicineOrderItemRequest, RemoveMedicineOrderItemResponse, RemoveMedicineOrderItemHttpResponse>
    > _dictionary;

    internal RemoveMedicineOrderItemHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveMedicineOrderItemResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveMedicineOrderItemResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveMedicineOrderItemResponseStatusCode.MEDICINE_ORDER_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: RemoveMedicineOrderItemResponseStatusCode.MEDICINE_ITEM_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: RemoveMedicineOrderItemResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

    }

    internal Func<
        RemoveMedicineOrderItemRequest,
        RemoveMedicineOrderItemResponse,
        RemoveMedicineOrderItemHttpResponse
    > Resolve(RemoveMedicineOrderItemResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
