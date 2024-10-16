using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.MedicineOrders.RemoveOrderItems;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.MedicineOrders.OrderMedicines;

/// <summary>
///     GetMedicineOrderItems Handler
/// </summary>
public class OrderMedicinesHandler
    : IFeatureHandler<OrderMedicinesRequest, OrderMedicinesResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public OrderMedicinesHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor contextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    public async Task<OrderMedicinesResponse> ExecuteAsync(
        OrderMedicinesRequest request,
        CancellationToken cancellationToken
    )
    {

        // Get role from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        // Only staff - doctor can access
        if (!role.Equals("staff") && !role.Equals("doctor"))
        {
            return new OrderMedicinesResponse()
            {
                StatusCode = OrderMedicinesResponseStatusCode.FORBIDDEN,
            };
        }

        // check medicinOrder is exist
        var isMedicineOrderExist = await _unitOfWork.OrderMedicinesRepostitory
            .IsMedicineOrderExist(
                medicineOrderId: request.MedicineOrderId,
                cancellationToken: cancellationToken
            );
        if (!isMedicineOrderExist)
        {
            return new OrderMedicinesResponse()
            {
                StatusCode = OrderMedicinesResponseStatusCode.MEDICINE_ORDER_NOT_FOUND
            };
        }

        // check medicine is available (on warehouse)
        var isMedicineAvailable = await _unitOfWork.OrderMedicinesRepostitory
            .IsMedicineAvailable(
                medicineId: request.MedicineId,
                cancellationToken: cancellationToken
            );

        if (!isMedicineAvailable)
        {
            return new OrderMedicinesResponse()
            {
                StatusCode = OrderMedicinesResponseStatusCode.MEDICINE_NOT_AVAILABLE,
            };
        }

        // check medicine already on medicineOrder
        var isMedicineAlreadyExist = await _unitOfWork.OrderMedicinesRepostitory
            .IsMedicineAlreadyExist(
                medicineOrderId: request.MedicineOrderId,
                medicineId: request.MedicineId,
                cancellationToken: cancellationToken
            );

        if(isMedicineAlreadyExist)
        {
            return new OrderMedicinesResponse()
            {
                StatusCode = OrderMedicinesResponseStatusCode.MEDICINE_ALREADY_EXIST,
            };
        }

        // order medicine
        var dbResult = await _unitOfWork.OrderMedicinesRepostitory
            .AddMedicineOrderItemCommandAsync(
                medicineOrderId: request.MedicineOrderId,
                medicineId: request.MedicineId,
                cancellationToken: cancellationToken
            );
        if(!dbResult)
        {
            return new OrderMedicinesResponse()
            {
                StatusCode = OrderMedicinesResponseStatusCode.DATABASE_OPERATION_FAILED ,

            };
        }

        // Response successfully.
        return new OrderMedicinesResponse()
        {
            StatusCode = OrderMedicinesResponseStatusCode.OPERATION_SUCCESS,

        };

    }
}
