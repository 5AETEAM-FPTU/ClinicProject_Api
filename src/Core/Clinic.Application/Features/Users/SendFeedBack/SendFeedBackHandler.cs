using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Users.SendFeedBack;

internal sealed class SendFeedBackHandler
    : IFeatureHandler<SendFeedBackRequest, SendFeedBackResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public SendFeedBackHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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

    public async Task<SendFeedBackResponse> ExecuteAsync(
        SendFeedBackRequest command,
        CancellationToken ct
    )
    {
        //Get role
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        //Check if role is not user
        if (!Equals(objA: role, objB: "user"))
        {
            return new() { StatusCode = SendFeedBackResponseStatusCode.FORBIDEN_ACCESS };
        }

        //Check if feedback already existed
        var isExisted = await _unitOfWork.SendFeedBackRepository.IsExistFeedBack(
            command.AppointmentId,
            cancellationToken: ct
        );

        if (isExisted)
        {
            return new() { StatusCode = SendFeedBackResponseStatusCode.FEEDBACK_IS_ALREADY_SENT };
        }

        //Check if appointment not completed
        var isAppointmentCompleted = await _unitOfWork.SendFeedBackRepository.IsAppointmentDone(
            command.AppointmentId,
            cancellationToken: ct
        );

        if (!isAppointmentCompleted)
        {
            return new()
            {
                StatusCode = SendFeedBackResponseStatusCode.APPOINTMENT_IS_NOT_COMPLETED,
            };
        }

        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        //Create new Feedback
        var newFeedBack = new Feedback()
        {
            Id = Guid.NewGuid(),
            AppointmentId = command.AppointmentId,
            Comment = command.Comment,
            Vote = command.Vote,
            CreatedBy = userId,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(
                dateTime: DateTime.UtcNow,
                destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(
                    id: "SE Asia Standard Time"
                )
            ),
        };

        //Create feedback into database
        var dbResult = await _unitOfWork.SendFeedBackRepository.CreateNewFeedBack(
            newFeedBack,
            cancellationToken: ct
        );

        //Check if operation failed
        if (!dbResult)
        {
            return new() { StatusCode = SendFeedBackResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        //Return successful code
        return new() { StatusCode = SendFeedBackResponseStatusCode.OPERATION_SUCCESS };
    }
}
