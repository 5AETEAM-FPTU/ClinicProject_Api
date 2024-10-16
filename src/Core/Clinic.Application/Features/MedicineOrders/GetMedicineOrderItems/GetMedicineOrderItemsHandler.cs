using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.MedicineOrders.GetMedicineOrderItems;

/// <summary>
///     GetMedicineOrderItems Handler
/// </summary>
public class GetMedicineOrderItemsHandler
    : IFeatureHandler<GetMedicineOrderItemsRequest, GetMedicineOrderItemsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetMedicineOrderItemsHandler(
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
    public async Task<GetMedicineOrderItemsResponse> ExecuteAsync(
        GetMedicineOrderItemsRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // check serviceOrder is exist
        var isMedicineOrderExist = await _unitOfWork.GetMedicineOrderItemsRepostitory
            .IsMedicineOrderExist(
                medicineOrderId: request.MedicineOrderId,
                CancellationToken: cancellationToken
            );
        if (!isMedicineOrderExist)
        {
            return new GetMedicineOrderItemsResponse()
            {
                StatusCode = GetMedicineOrderItemsResponseStatusCode.SERVICE_ORDER_NOT_FOUND
            };
        }


        // get service order items
        var medicineOrder = await _unitOfWork.GetMedicineOrderItemsRepostitory
            .GetMedicineOrderItemsQueryAsync(
                medicineOrderId: request.MedicineOrderId,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetMedicineOrderItemsResponse()
        {
            StatusCode = GetMedicineOrderItemsResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                MedicineOrder = new GetMedicineOrderItemsResponse.Body.MedicineOrderDetail()
                {

                    Id = request.MedicineOrderId,
                    TotalItem = medicineOrder.MedicineOrderItems.Count(),

                    Items = medicineOrder.MedicineOrderItems
                        .Select(medicineOrderItem => new GetMedicineOrderItemsResponse.Body.MedicineOrderDetail.MedicineOrderItem()
                        {
                            Quantity = medicineOrderItem.Quantity,
                            Description = medicineOrderItem.Description,

                            Medicine = new GetMedicineOrderItemsResponse.Body.MedicineOrderDetail.MedicineOrderItem.MedicineDetail()
                            {
                                Id = medicineOrderItem.Medicine.Id,
                                Name = medicineOrderItem.Medicine.Name,

                                Type = new GetMedicineOrderItemsResponse.Body.MedicineOrderDetail.MedicineOrderItem.MedicineDetail.MedicineTypeDetail()
                                {
                                    Id = medicineOrderItem.Medicine.MedicineType.Id,
                                    Name = medicineOrderItem.Medicine.MedicineType.Name,
                                    Constant = medicineOrderItem.Medicine.MedicineType.Constant
                                }
                            }
                        })

                }
            }
        };

    }
}
