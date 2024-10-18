using System;
using System.Collections.Generic;
using Clinic.Application.Features.MedicineOrders.UpdateNoteMedicineOrder;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicineOrders.UpdateNoteMedicineOrder.HttpResponseMapper;

/// <summary>
///     Mapper for UpdateNoteMedicineOrder feature
/// </summary>
public class UpdateNoteMedicineOrderHttpResponseManager
{
    private readonly Dictionary<
        UpdateNoteMedicineOrderResponseStatusCode,
        Func<UpdateNoteMedicineOrderRequest, UpdateNoteMedicineOrderResponse, UpdateNoteMedicineOrderHttpResponse>
    > _dictionary;

    internal UpdateNoteMedicineOrderHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateNoteMedicineOrderResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateNoteMedicineOrderResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateNoteMedicineOrderResponseStatusCode.MEDICINE_ORDER_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdateNoteMedicineOrderResponseStatusCode.FORBIDDEN,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

    }

    internal Func<
        UpdateNoteMedicineOrderRequest,
        UpdateNoteMedicineOrderResponse,
        UpdateNoteMedicineOrderHttpResponse
    > Resolve(UpdateNoteMedicineOrderResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
