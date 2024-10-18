
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.ServiceOrders.AddOrderService;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.MedicineOrders.RemoveOrderItems;

/// <summary>
///     RemoveMedicineOrderItem Handler
/// </summary>
public class RemoveMedicineOrderItemHandler
    : IFeatureHandler<RemoveMedicineOrderItemRequest, RemoveMedicineOrderItemResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public RemoveMedicineOrderItemHandler(
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
    public async Task<RemoveMedicineOrderItemResponse> ExecuteAsync(
        RemoveMedicineOrderItemRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get role from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        // Only staff - doctor can access
        if (!role.Equals("staff") && !role.Equals("doctor"))
        {
            return new RemoveMedicineOrderItemResponse()
            {
                StatusCode = RemoveMedicineOrderItemResponseStatusCode.FORBIDDEN,
            };
        }

        // check medicinOrder is exist
        var isMedicineOrderExist = await _unitOfWork.RemoveMedicineOrderItemRepository
            .IsMedicineOrderExist(
                medicineOrderId: request.MedicineOrderId,
                cancellationToken: cancellationToken
            );

        if (!isMedicineOrderExist)
        {
            return new RemoveMedicineOrderItemResponse()
            {
                StatusCode = RemoveMedicineOrderItemResponseStatusCode.MEDICINE_ORDER_NOT_FOUND
            };
        }

        // check medicine already on medicineOrder
        var isMedicineAlreadyExist = await _unitOfWork.RemoveMedicineOrderItemRepository
            .IsMedicineAlreadyExist(
                medicineOrderId: request.MedicineOrderId,
                medicineId: request.MedicineId,
                cancellationToken: cancellationToken
            );

        if (!isMedicineAlreadyExist)
        {
            return new RemoveMedicineOrderItemResponse()
            {
                StatusCode = RemoveMedicineOrderItemResponseStatusCode.MEDICINE_ITEM_NOT_FOUND,
            };
        }

        // update medicine order items
        var dbResult = await _unitOfWork.RemoveMedicineOrderItemRepository
            .UpdateMedicineOrderItemCommandAsync(
                medicineOrderId: request.MedicineOrderId,
                medicineId: request.MedicineId,
                cancellationToken: cancellationToken
            );

        if (!dbResult)
        {
            return new RemoveMedicineOrderItemResponse()
            {
                StatusCode = RemoveMedicineOrderItemResponseStatusCode.DATABASE_OPERATION_FAILED,

            };
        }

        // Response successfully.
        return new RemoveMedicineOrderItemResponse()
        {
            StatusCode = RemoveMedicineOrderItemResponseStatusCode.OPERATION_SUCCESS,

        };

    }
}
