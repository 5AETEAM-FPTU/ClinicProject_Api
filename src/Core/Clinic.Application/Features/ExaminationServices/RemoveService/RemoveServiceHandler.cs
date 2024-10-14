using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.ExaminationServices.RemoveService;

/// <summary>
///     RemoveService Handler
/// </summary>
public class RemoveServiceHandler
    : IFeatureHandler<RemoveServiceRequest, RemoveServiceResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private IDefaultUserAvatarAsUrlHandler _defaultUserAvatarAsUrlHandler;
    private readonly IHttpContextAccessor _contextAccessor;

    public RemoveServiceHandler(
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
    public async Task<RemoveServiceResponse> ExecuteAsync(
        RemoveServiceRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userRole from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!(Equals(objA: role, objB: "admin") || Equals(objA: role, objB: "staff")))
        {
            return new() { StatusCode = RemoveServiceResponseStatusCode.FORBIDEN };
        }

        // check service is existed
        var isServiceExisted = await _unitOfWork.RemoveServiceRepository
            .IsServiceExisted(
                serviceId: request.ServiceId,
                cancellationToken: cancellationToken
            );

        if (!isServiceExisted)
        {
            return new RemoveServiceResponse()
            {
                StatusCode = RemoveServiceResponseStatusCode.SERVICE_NOT_FOUND
            };
        }

        // Database operation
        var dbResult = await _unitOfWork.RemoveServiceRepository.RemoveServiceByIdCommandAsync(
            Id: request.ServiceId,
            cancellationToken: cancellationToken
        );

        // Respond if database operation failed.
        if (!dbResult)
        {
            return new() { StatusCode = RemoveServiceResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Response successfully.
        return new RemoveServiceResponse()
        {
            StatusCode = RemoveServiceResponseStatusCode.OPERATION_SUCCESS,
        };
    }

}
