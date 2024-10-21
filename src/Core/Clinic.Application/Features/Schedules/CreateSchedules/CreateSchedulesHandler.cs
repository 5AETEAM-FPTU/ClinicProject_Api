using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Schedules.CreateSchedules;

/// <summary>
///     CreateSchedules Handler
/// </summary>
public class CreateSchedulesHandler
    : IFeatureHandler<CreateSchedulesRequest, CreateSchedulesResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private IDefaultUserAvatarAsUrlHandler _defaultUserAvatarAsUrlHandler;
    private readonly IHttpContextAccessor _contextAccessor;

    public CreateSchedulesHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IDefaultUserAvatarAsUrlHandler defaultUserAvatarAsUrlHandlerl,
        IHttpContextAccessor contextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
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
    public async Task<CreateSchedulesResponse> ExecuteAsync(
        CreateSchedulesRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!(Equals(objA: role, objB: "doctor") || Equals(objA: role, objB: "staff")))
        {
            return new() { StatusCode = CreateSchedulesResponseStatusCode.FORBIDEN };
        }

        // Init schedules from time slot
        var doctorId = Guid.Empty;
        if (request.DoctorId == null) {
            doctorId = Guid.Parse(_contextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub));
        } else doctorId = (Guid)request.DoctorId;

        var newSchedules = InitSchedules(
            request: request,
            doctorId: doctorId
        );

        // Check overlapping in database
        var isOverlapping = await _unitOfWork.CreateSchedulesRepository.AreOverLappedSlotTimes(
            createSchedules: newSchedules,
            cancellationToken: cancellationToken
        );

        // Respond if overlapping.
        if (isOverlapping)
        {
            return new() { StatusCode = CreateSchedulesResponseStatusCode.TIMESLOT_IS_EXIST };
        }

        // Database operation
        var dbResult = await _unitOfWork.CreateSchedulesRepository.CreateSchedulesAsync(
            createSchedules: newSchedules,
            cancellationToken: cancellationToken
        );

        // Respond if database operation failed.
        if (!dbResult)
        {
            return new() { StatusCode = CreateSchedulesResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Response successfully.
        return new CreateSchedulesResponse()
        {
            StatusCode = CreateSchedulesResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private static IEnumerable<Schedule> InitSchedules(
        CreateSchedulesRequest request,
        Guid doctorId
    )
    {
        return request.TimeSlots.Select(selector: timeSlot => new Schedule()
        {
            StartDate = timeSlot.StartTime,
            EndDate = timeSlot.EndTime,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = doctorId,
            DoctorId = doctorId
        });
    }
}
