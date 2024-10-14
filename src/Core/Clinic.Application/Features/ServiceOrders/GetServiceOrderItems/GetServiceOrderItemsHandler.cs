using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.ServiceOrders.GetServiceOrderItems;

/// <summary>
///     GetServiceOrderItems Handler
/// </summary>
public class GetServiceOrderItemsHandler
    : IFeatureHandler<GetServiceOrderItemsRequest, GetServiceOrderItemsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetServiceOrderItemsHandler(
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
    public async Task<GetServiceOrderItemsResponse> ExecuteAsync(
        GetServiceOrderItemsRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // check serviceOrder is exist
        var isServiceOrderExist = await _unitOfWork.GetServiceOrderItemsRepository
            .IsServiceOrderExist(
                serviceOrderId: request.ServiceOrderId,
                CancellationToken: cancellationToken
            );
        if (!isServiceOrderExist) {
            return new GetServiceOrderItemsResponse()
            {
                StatusCode = GetServiceOrderItemsResponseStatusCode.SERVICE_ORDER_NOT_FOUND
            };
        }


        // get service order items
        var serviceOrder = await _unitOfWork.GetServiceOrderItemsRepository
            .GetServiceOrderItemsQueryAsync(
                serviceOrderId: request.ServiceOrderId,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetServiceOrderItemsResponse()
        {
            StatusCode = GetServiceOrderItemsResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                ServiceOrder = new GetServiceOrderItemsResponse.Body.ServiceOrderDetail()
                {

                    Id = request.ServiceOrderId,
                    Quantity = serviceOrder.ServiceOrderItems.Count(),
                    TotalPrice = serviceOrder.ServiceOrderItems.Sum(entity => entity.PriceAtOrder),
                    IsAllUpdated = serviceOrder.ServiceOrderItems.All(entity => entity.IsUpdated),

                    Items = serviceOrder.ServiceOrderItems
                        .Select(serviceOrderItem => new GetServiceOrderItemsResponse.Body.ServiceOrderDetail.ServiceOrderItem()
                        {
                            PriceAtOrder = serviceOrderItem.PriceAtOrder,
                            IsUpdated = serviceOrderItem.IsUpdated,                     // check
                            Service = new GetServiceOrderItemsResponse.Body.ServiceOrderDetail.ServiceOrderItem.ServiceDetail()
                            {
                                Id = serviceOrderItem.Service.Id,
                                Code = serviceOrderItem.Service.Code,
                                Name = serviceOrderItem.Service.Name,
                            }
                        })

                }
            }
        };

    }
}
