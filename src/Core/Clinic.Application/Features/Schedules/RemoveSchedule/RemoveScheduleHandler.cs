using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Schedules.RemoveSchedule;

/// <summary>
///     CreateSchedules Handler
/// </summary>
public class RemoveScheduleHandler
    : IFeatureHandler<RemoveScheduleRequest, RemoveScheduleResponse>
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
    public async Task<RemoveScheduleResponse> ExecuteAsync(
        RemoveScheduleRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userRole from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!(Equals(objA: role, objB: "doctor") || Equals(objA: role, objB: "staff")))
        {
            return new() { StatusCode = RemoveScheduleResponseStatusCode.FORBIDEN };
        }       

        // Check schedule is exist or not
        var isScheduleExist = await _unitOfWork.RemoveScheduleRepository.IsScheduleExist(
            scheduleId: request.ScheduleId
            );

        // Respond if schedule not exsit
        if (!isScheduleExist)
        {
            return new() { StatusCode = RemoveScheduleResponseStatusCode.NOT_FOUND_SCHEDULE };
        }

        // Check schedule had appointment 
        var isScheduleHadAppointment = await _unitOfWork.RemoveScheduleRepository.IsScheduleHadAppointment(
            scheduleId: request.ScheduleId,
            cancellationToken: cancellationToken
        );

        // Respond if overlapping.
        if (isScheduleHadAppointment)
        {
            return new() { StatusCode = RemoveScheduleResponseStatusCode.SCHEDULE_HAD_APPOINTMENT };
        }

        // Database operation
        var dbResult = await _unitOfWork.RemoveScheduleRepository.RemoveScheduleByIdCommandAsync(
            scheduleId: request.ScheduleId,
            cancellationToken: cancellationToken
        );

        // Respond if database operation failed.
        if (!dbResult)
        {
            return new() { StatusCode = RemoveScheduleResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Response successfully.
        return new RemoveScheduleResponse()
        {
            StatusCode = RemoveScheduleResponseStatusCode.OPERATION_SUCCESS,
        };
    }

}
