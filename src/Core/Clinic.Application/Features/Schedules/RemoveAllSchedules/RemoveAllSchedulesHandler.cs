using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Application.Features.Schedules.CreateSchedules;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Schedules.RemoveAllSchedules;

/// <summary>
///     CreateSchedules Handler
/// </summary>
public class RemoveScheduleHandler
    : IFeatureHandler<RemoveAllSchedulesRequest, RemoveAllSchedulesResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private IDefaultUserAvatarAsUrlHandler _defaultUserAvatarAsUrlHandler;
    private readonly IHttpContextAccessor _contextAccessor;

    public RemoveScheduleHandler(
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
    public async Task<RemoveAllSchedulesResponse> ExecuteAsync(
        RemoveAllSchedulesRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userRole from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!(Equals(objA: role, objB: "doctor") || Equals(objA: role, objB: "staff")))
        {
            return new() { StatusCode = RemoveAllSchedulesResponseStatusCode.FORBIDEN };
        }

        // Get userId from sub type jwt
        var doctorId = Guid.Empty;
        if (request.DoctorId == null)
        {
            doctorId = Guid.Parse(
                _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
            );
        } else doctorId = (Guid)request.DoctorId;

        // Database operation
        var dbResult = await _unitOfWork.RemoveAllSchedulesRepository.RemoveAllSchedulesByDateCommandAsync(
            doctorId: doctorId,
            date: request.Date,
            cancellationToken: cancellationToken
        );

        // Respond if database operation failed.
        if (!dbResult)
        {
            return new() { StatusCode = RemoveAllSchedulesResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Response successfully.
        return new RemoveAllSchedulesResponse()
        {
            StatusCode = RemoveAllSchedulesResponseStatusCode.OPERATION_SUCCESS,
        };
    }

}
