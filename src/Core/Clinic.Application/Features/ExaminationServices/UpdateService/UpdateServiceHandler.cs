using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Clinic.Application.Features.ExaminationServices.CreateService;

namespace Clinic.Application.Features.ExaminationServices.UpdateService;

/// <summary>
///     UpdateService Handler
/// </summary>

internal sealed class UpdateServiceHandler
    : IFeatureHandler<UpdateServiceRequest, UpdateServiceResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateServiceHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    /// <summary>
    /// Empty implementation.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="ct"></param>
    /// <returns></returns> <summary>
    ///
    /// </summary>
    /// <param name="command"></param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///  A task containing the response.
    /// </returns>

    public async Task<UpdateServiceResponse> ExecuteAsync(
        UpdateServiceRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check role user from role type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("admin") && !role.Equals("staff"))
        {
            return new()
            {
                StatusCode = UpdateServiceResponseStatusCode.ROLE_IS_NOT_ADMIN_STAFF,
            };
        }

        //Check service is existed
        var isServiceExisted = await _unitOfWork.UpdateServiceRepository
            .IsServiceExist(
                request.ServiceId, 
                cancellationToken: cancellationToken
            );

        if (!isServiceExisted)
        {
            return new()
            {
                StatusCode = UpdateServiceResponseStatusCode.SERVICE_NOT_FOUND,
            };
        }

        // check service code is existed
        if (request.Code != null)
        {
            var isServiceCodeExisted = await _unitOfWork.UpdateServiceRepository.IsExistServiceCode(request.Code, cancellationToken: cancellationToken);

            if (isServiceCodeExisted)
            {
                return new()
                {
                    StatusCode = UpdateServiceResponseStatusCode.SERVICE_CODE_ALREADY_EXISTED,
                };
            }
        }

        // update service
        var existedService =  await _unitOfWork.UpdateServiceRepository
            .GetServiceByIdCommandAsync(
                Id: request.ServiceId, 
                cancellationToken: cancellationToken
            );     

        var isSucceed = await UpdateService(request, existedService, cancellationToken);

        // Check if operation failed
        if (!isSucceed)
        {
            return new()
            {
                StatusCode = UpdateServiceResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Return successful
        return new()
        {
            StatusCode = UpdateServiceResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private async Task<bool> UpdateService(
        UpdateServiceRequest request, 
        Service existedService, 
        CancellationToken cancellationToken
        )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // update service
        existedService.Code = request.Code ?? existedService.Code;
        existedService.Name = request.Name ?? existedService.Name;
        existedService.Descripiton = request.Description ??  existedService.Descripiton;
    
        if (request.Price != null)
        {
            existedService.Price = (decimal)request.Price;
        }
        
        existedService.Group = request.Group ?? existedService.Group;
        existedService.UpdatedAt = DateTime.UtcNow;
        existedService.UpdatedBy = userId;

        return await _unitOfWork.UpdateServiceRepository
            .UpdateServiceCommandAsync(Service: existedService, cancellationToken);
      
    }

}
