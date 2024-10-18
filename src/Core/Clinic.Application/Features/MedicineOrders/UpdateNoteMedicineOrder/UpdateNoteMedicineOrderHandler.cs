
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.MedicineOrders.UpdateNoteMedicineOrder;

/// <summary>
///     UpdateMedicineOrderItem Handler
/// </summary>
public class UpdateNoteMedicineOrderHandler
    : IFeatureHandler<UpdateNoteMedicineOrderRequest, UpdateNoteMedicineOrderResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateNoteMedicineOrderHandler(
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
    public async Task<UpdateNoteMedicineOrderResponse> ExecuteAsync(
        UpdateNoteMedicineOrderRequest request,
        CancellationToken cancellationToken
    )
    {

        // Get role from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        // Only staff - doctor can access
        if (!role.Equals("staff") && !role.Equals("doctor"))
        {
            return new UpdateNoteMedicineOrderResponse()
            {
                StatusCode = UpdateNoteMedicineOrderResponseStatusCode.FORBIDDEN,
            };
        }

        // check medicinOrder is exist
        var isMedicineOrderExist = await _unitOfWork.UpdateNoteMedicineOrderRepository
            .IsMedicineOrderExist(
                medicineOrderId: request.MedicineOrderId,
                cancellationToken: cancellationToken
            );
        if (!isMedicineOrderExist)
        {
            return new UpdateNoteMedicineOrderResponse()
            {
                StatusCode = UpdateNoteMedicineOrderResponseStatusCode.MEDICINE_ORDER_NOT_FOUND
            };
        }

        // update medicine order items
        var dbResult = await _unitOfWork.UpdateNoteMedicineOrderRepository
            .UpdateNoteMedicineOrderCommandAsync(
                medicineOrderId: request.MedicineOrderId,
                note: request.Note,
                cancellationToken: cancellationToken
            );

        if (!dbResult)
        {
            return new UpdateNoteMedicineOrderResponse()
            {
                StatusCode = UpdateNoteMedicineOrderResponseStatusCode.DATABASE_OPERATION_FAILED,

            };
        }

        // Response successfully.
        return new UpdateNoteMedicineOrderResponse()
        {
            StatusCode = UpdateNoteMedicineOrderResponseStatusCode.OPERATION_SUCCESS,

        };

    }
}
