using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.ExaminationServices.CreateService;

/// <summary>
///     CreateService Handler
/// </summary>

internal sealed class CreateServiceHandler
    : IFeatureHandler<CreateServiceRequest, CreateServiceResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public CreateServiceHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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

    public async Task<CreateServiceResponse> ExecuteAsync(
        CreateServiceRequest request,
        CancellationToken ct
    )
    {
        // Check role user from role type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("admin") && !role.Equals("staff"))
        {
            return new()
            {
                StatusCode = CreateServiceResponseStatusCode.ROLE_IS_NOT_ADMIN_STAFF,
            };
        }

        //Check if service already existed
        var isServiceCodeExisted = await _unitOfWork.CreateServiceRepository.IsExistServiceCode(request.Code, cancellationToken: ct);

        if (isServiceCodeExisted)
        {
            return new()
            {
                StatusCode = CreateServiceResponseStatusCode.SERVICE_CODE_ALREADY_EXISTED,
            };
        }

        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Init service
        var newService = InitService(request, userId);

        // Create new service
        var dbResult = await _unitOfWork.CreateServiceRepository.CreateNewServiceCommandAsync(
            service: newService,
            cancellationToken: ct
        );

        // Check if operation failed
        if (!dbResult)
        {
            return new()
            {
                StatusCode = CreateServiceResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Return successful
        return new()
        {
            StatusCode = CreateServiceResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private static Service InitService(
        CreateServiceRequest request,
        Guid createdBy)
    {
        //Create new service
        return new Service()
        {
            Id = Guid.NewGuid(),
            Code = request.Code,
            Name = request.Name,
            Descripiton = request.Description,
            Price = request.Price,
            Group = request.Group,  

            CreatedBy = createdBy,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(
                dateTime: DateTime.UtcNow,
                destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(
                    id: "SE Asia Standard Time"
                )
            ),
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
        };
    }

}
