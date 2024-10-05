using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Schedules.GetSchedulesByDate;

/// <summary>
///     GetSchedulesByDate Handler //
/// </summary>
public class GetSchedulesByDateHandler
    : IFeatureHandler<GetSchedulesByDateRequest, GetSchedulesByDateResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private IHttpContextAccessor _contextAccessor;

    public GetSchedulesByDateHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetSchedulesByDateResponse> ExecuteAsync(
        GetSchedulesByDateRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Found user by userId
        var foundUser = await _unitOfWork.GetScheduleDatesByMonthRepository.GetUserByIdAsync(
            userId,
            cancellationToken
        );

        // Handle date
        var startDate = request.Date.Date;
        var endDate = startDate.AddDays(1).AddTicks(-1);

        // Get Schedules
        var schedules = await _unitOfWork.GetSchedulesByDateRepository.GetSchedulesByDateQueryAsync(
            startDate: startDate,
            endDate: endDate,
            doctorId: request.DoctorId != default ? (Guid)request.DoctorId : userId,
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetSchedulesByDateResponse()
        {
            StatusCode = GetSchedulesByDateResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                TimeSlots = schedules
                    .Select(schedule => new GetSchedulesByDateResponse.Body.TimeSlot()
                    {
                        SlotId = schedule.Id,
                        StartTime = schedule.StartDate,
                        EndTime = schedule.EndDate,
                        IsHadAppointment = schedule.Appointment != null
                    })
                    .ToList()
            }
        };
    }
}
