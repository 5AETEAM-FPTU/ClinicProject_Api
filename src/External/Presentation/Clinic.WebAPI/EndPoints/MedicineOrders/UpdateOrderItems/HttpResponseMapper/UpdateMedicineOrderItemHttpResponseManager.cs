using System;
using System.Collections.Generic;
using Clinic.Application.Features.MedicineOrders.UpdateOrderItems;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicineOrders.UpdateOrderItems.HttpResponseMapper;

/// <summary>
///     Mapper for UpdateMedicineOrderItem feature
/// </summary>
public class UpdateMedicineOrderItemHttpResponseManager
{
    private readonly Dictionary<
        UpdateMedicineOrderItemResponseStatusCode,
        Func<UpdateMedicineOrderItemRequest, UpdateMedicineOrderItemResponse, UpdateMedicineOrderItemHttpResponse>
    > _dictionary;

    internal UpdateMedicineOrderItemHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateMedicineOrderItemResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateMedicineOrderItemResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateMedicineOrderItemResponseStatusCode.MEDICINE_ORDER_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdateMedicineOrderItemResponseStatusCode.MEDICINE_ITEM_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdateMedicineOrderItemResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

    }

    internal Func<
        UpdateMedicineOrderItemRequest,
        UpdateMedicineOrderItemResponse,
        UpdateMedicineOrderItemHttpResponse
    > Resolve(UpdateMedicineOrderItemResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
