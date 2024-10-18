
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.MedicineOrders.UpdateOrderItems;

/// <summary>
///     UpdateMedicineOrderItem Handler
/// </summary>
public class UpdateMedicineOrderItemHandler
    : IFeatureHandler<UpdateMedicineOrderItemRequest, UpdateMedicineOrderItemResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateMedicineOrderItemHandler(
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
    public async Task<UpdateMedicineOrderItemResponse> ExecuteAsync(
        UpdateMedicineOrderItemRequest request,
        CancellationToken cancellationToken
    )
    {

        // Get role from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        // Only staff - doctor can access
        if (!role.Equals("staff") && !role.Equals("doctor"))
        {
            return new UpdateMedicineOrderItemResponse()
            {
                StatusCode = UpdateMedicineOrderItemResponseStatusCode.FORBIDDEN,
            };
        }

        // check medicinOrder is exist
        var isMedicineOrderExist = await _unitOfWork.UpdateMedicineOrderItemRepository
            .IsMedicineOrderExist(
                medicineOrderId: request.MedicineOrderId,
                cancellationToken: cancellationToken
            );
        if (!isMedicineOrderExist)
        {
            return new UpdateMedicineOrderItemResponse()
            {
                StatusCode = UpdateMedicineOrderItemResponseStatusCode.MEDICINE_ORDER_NOT_FOUND
            };
        }

        // check medicine already on medicineOrder
        var isMedicineAlreadyExist = await _unitOfWork.UpdateMedicineOrderItemRepository
            .IsMedicineAlreadyExist(
                medicineOrderId: request.MedicineOrderId,
                medicineId: request.MedicineId,
                cancellationToken: cancellationToken
            );

        if (!isMedicineAlreadyExist)
        {
            return new UpdateMedicineOrderItemResponse()
            {
                StatusCode = UpdateMedicineOrderItemResponseStatusCode.MEDICINE_ITEM_NOT_FOUND,
            };
        }

        // update medicine order items
        var dbResult = await _unitOfWork.UpdateMedicineOrderItemRepository
            .UpdateMedicineOrderItemCommandAsync(
                medicineOrderId: request.MedicineOrderId,
                medicineId: request.MedicineId,
                quantity: request.Quantity,
                description: request.Description,
                cancellationToken: cancellationToken
            );

        if (!dbResult)
        {
            return new UpdateMedicineOrderItemResponse()
            {
                StatusCode = UpdateMedicineOrderItemResponseStatusCode.DATABASE_OPERATION_FAILED,

            };
        }

        // Response successfully.
        return new UpdateMedicineOrderItemResponse()
        {
            StatusCode = UpdateMedicineOrderItemResponseStatusCode.OPERATION_SUCCESS,

        };

    }
}
