using System;
using System.Collections.Generic;
using Clinic.Application.Features.MedicineOrders.OrderMedicines;
using Microsoft.AspNetCore.Http;

namespace Clinic.WebAPI.EndPoints.MedicineOrders.OrderMedicines.HttpResponseMapper;

/// <summary>
///     Mapper for GetMedicineOrderItems feature
/// </summary>
public class OrderMedicinesHttpResponseManager
{
    private readonly Dictionary<
        OrderMedicinesResponseStatusCode,
        Func<OrderMedicinesRequest, OrderMedicinesResponse, OrderMedicinesHttpResponse>
    > _dictionary;

    internal OrderMedicinesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: OrderMedicinesResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: OrderMedicinesResponseStatusCode.DATABASE_OPERATION_FAILED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: OrderMedicinesResponseStatusCode.MEDICINE_ORDER_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: OrderMedicinesResponseStatusCode.MEDICINE_NOT_AVAILABLE,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: OrderMedicinesResponseStatusCode.MEDICINE_ALREADY_EXIST,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

    }

    internal Func<
        OrderMedicinesRequest,
        OrderMedicinesResponse,
        OrderMedicinesHttpResponse
    > Resolve(OrderMedicinesResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
