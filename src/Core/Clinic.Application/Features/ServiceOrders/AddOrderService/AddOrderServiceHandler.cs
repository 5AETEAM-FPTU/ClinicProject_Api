using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.ServiceOrders.GetServiceOrderItems;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.ServiceOrders.AddOrderService;

/// <summary>
///     AddOrderService Handler
/// </summary>
public class AddOrderServiceHandler
    : IFeatureHandler<AddOrderServiceRequest, AddOrderServiceResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public AddOrderServiceHandler(
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
    public async Task<AddOrderServiceResponse> ExecuteAsync(
        AddOrderServiceRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get role from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        
        // Only staff - doctor can access
        if (!role.Equals("staff") && !role.Equals("doctor")) 
        {
            return new AddOrderServiceResponse()
            {
                StatusCode = AddOrderServiceResponseStatusCode.FORBIDDEN,
            };
        }

        // check serviceOrder is exist
        var isServiceOrderExist = await _unitOfWork.AddOrderServiceRepository
            .IsServiceOrderExist(
                serviceOrderId: request.ServiceOrderId,
                CancellationToken: cancellationToken
            );

        if (!isServiceOrderExist)
        {
            return new AddOrderServiceResponse()
            {
                StatusCode = AddOrderServiceResponseStatusCode.SERVICE_ORDER_NOT_FOUND
            };
        }

        // check services are available for ordering
        var AreServicesAvailable = await _unitOfWork.AddOrderServiceRepository
            .AreServicesAvailable(
                serviceIds: request.ServiceIds, 
                cancellationToken: cancellationToken
            );

        if (!AreServicesAvailable)
        {
            return new AddOrderServiceResponse()
            {
                StatusCode = AddOrderServiceResponseStatusCode.SERVICE_NOT_AVAILABLE,
            };
        }

        // add service order items
        var serviceOrder = await _unitOfWork.AddOrderServiceRepository
            .AddServiceIntoServiceOrderCommandAsync(
                serviceOrderId: request.ServiceOrderId,
                serviceIds: request.ServiceIds,
                cancellationToken: cancellationToken
            );

        // Response if db operation failed
        if (!serviceOrder)
        {
            return new AddOrderServiceResponse()
            {
                StatusCode = AddOrderServiceResponseStatusCode.DATABASE_OPERATION_FAILED,
            };
        }

        // update total price and quantity in OrderService and MedicalReport
        var updateResult = await _unitOfWork.AddOrderServiceRepository
            .UpdateTotalPriceRelatedTableCommandAsync
            (
                serviceOrderId: request.ServiceOrderId,
                cancellationToken: cancellationToken
            );
        if (!updateResult)
        {
            return new AddOrderServiceResponse()
            {
                StatusCode = AddOrderServiceResponseStatusCode.DATABASE_OPERATION_FAILED,
            };
        }

        // Response successfully.
        return new AddOrderServiceResponse()
        {
            StatusCode = AddOrderServiceResponseStatusCode.OPERATION_SUCCESS,
        };

    }
}
