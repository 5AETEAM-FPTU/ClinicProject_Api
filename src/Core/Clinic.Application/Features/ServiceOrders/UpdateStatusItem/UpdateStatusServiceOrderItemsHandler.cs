using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.ServiceOrders.UpdateStatusItem;

/// <summary>
///     UpdateStatusServiceOrderItems Handler
/// </summary>
public class UpdateStatusServiceOrderItemsHandler
    : IFeatureHandler<UpdateStatusServiceOrderItemsRequest, UpdateStatusServiceOrderItemsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateStatusServiceOrderItemsHandler(
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
    public async Task<UpdateStatusServiceOrderItemsResponse> ExecuteAsync(
        UpdateStatusServiceOrderItemsRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        // Only staff - doctor can access
        if (!role.Equals("staff") && !role.Equals("doctor"))
        {
            return new UpdateStatusServiceOrderItemsResponse()
            {
                StatusCode = UpdateStatusServiceOrderItemsResponseStatusCode.FORBIDDEN,
            };
        }

        // check serviceOrderItem is exist
        var isServiceItemExist = await _unitOfWork.UpdateStatusServiceOrderItemRepository.IsServiceItemExist(
            serviceOrderId: request.ServiceOrderId,
            serviceId: request.ServiceId
        );

        if (!isServiceItemExist)
        {
            return new UpdateStatusServiceOrderItemsResponse()
            {
                StatusCode = UpdateStatusServiceOrderItemsResponseStatusCode.SERVICE_ITEM_NOT_FOUND,
            };
        }

        var dbResult = await _unitOfWork.UpdateStatusServiceOrderItemRepository.UpdateStatusServiceOrderItemCommandAsync(
                serviceOrderId: request.ServiceOrderId,
                serviceId: request.ServiceId,
                cancellationToken: cancellationToken
            );

        if(!dbResult)
        {
            return new UpdateStatusServiceOrderItemsResponse()
            {
                StatusCode = UpdateStatusServiceOrderItemsResponseStatusCode.DATABASE_OPERATION_FAILED,
            };
        }

        // Response successfully.
        return new UpdateStatusServiceOrderItemsResponse()
        {
            StatusCode = UpdateStatusServiceOrderItemsResponseStatusCode.OPERATION_SUCCESS,
            
        };

    }
}
