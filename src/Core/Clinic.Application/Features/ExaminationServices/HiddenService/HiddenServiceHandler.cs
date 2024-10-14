using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Application.Features.ExaminationServices.RemoveService;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.ExaminationServices.HiddenService;

/// <summary>
///     HiddenService Handler
/// </summary>
public class HiddenServiceHandler
    : IFeatureHandler<HiddenServiceRequest, HiddenServiceResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private IDefaultUserAvatarAsUrlHandler _defaultUserAvatarAsUrlHandler;
    private readonly IHttpContextAccessor _contextAccessor;

    public HiddenServiceHandler(
        IUnitOfWork unitOfWork,
        IDefaultUserAvatarAsUrlHandler defaultUserAvatarAsUrlHandlerl,
        IHttpContextAccessor contextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _defaultUserAvatarAsUrlHandler = defaultUserAvatarAsUrlHandlerl;
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
    public async Task<HiddenServiceResponse> ExecuteAsync(
        HiddenServiceRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userRole from sub type jwt
        var userId = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "sub");
        // Get userRole from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!(Equals(objA: role, objB: "admin") || Equals(objA: role, objB: "staff")))
        {
            return new() { StatusCode = HiddenServiceResponseStatusCode.FORBIDEN };
        }

        // check service is existed
        var isServiceExisted = await _unitOfWork.RemoveServiceRepository
            .IsServiceExisted(
                serviceId: request.ServiceId,
                cancellationToken: cancellationToken
            );

        if (!isServiceExisted)
        {
            return new HiddenServiceResponse()
            {
                StatusCode = HiddenServiceResponseStatusCode.SERVICE_NOT_FOUND
            };
        }

        // Get service by id
        var foundService = await _unitOfWork.HiddenServiceRepository
            .GetServiceByIdQueryAsync(
                Id: request.ServiceId,
                cancellationToken: cancellationToken
            );

        // Handle Remove Temporarity
        foundService.RemovedAt = DateTime.UtcNow;
        foundService.RemovedBy = Guid.Parse(userId);


        // Database operation
        var dbResult = await _unitOfWork.HiddenServiceRepository.RemoveServiceTemporarityCommandAsync(
            service: foundService,
            cancellationToken: cancellationToken
        );

        // Respond if database operation failed.
        if (!dbResult)
        {
            return new() { StatusCode = HiddenServiceResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Response successfully.
        return new HiddenServiceResponse()
        {
            StatusCode = HiddenServiceResponseStatusCode.OPERATION_SUCCESS,
        };
    }

}
