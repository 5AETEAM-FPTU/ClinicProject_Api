using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.SMS;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Notification.CreateRetreatmentNotification;

internal sealed class CreateRetreatmentNotificationHandler
    : IFeatureHandler<CreateRetreatmentNotificationRequest, CreateRetreatmentNotificationResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ISmsHandler _smsHandler;

    public CreateRetreatmentNotificationHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor contextAccessor,
        ISmsHandler smsHandler
    )
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
        _smsHandler = smsHandler;
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

    public async Task<CreateRetreatmentNotificationResponse> ExecuteAsync(
        CreateRetreatmentNotificationRequest command,
        CancellationToken ct
    )
    {
        //Get role
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        //Check if role is not admin
        if (!(Equals(objA: role, objB: "doctor") || Equals(objA: role, objB: "staff")))
        {
            return new()
            {
                StatusCode = CreateRetreatmentNotificationResponseStatusCode.FORBIDEN_ACCESS,
            };
        }

        //Check if notification already existed
        var isExisted =
            await _unitOfWork.CreateRetreatmentNotificationRepository.IsExistNotification(
                command.ExaminationDate,
                cancellationToken: ct
            );

        if (isExisted)
        {
            return new()
            {
                StatusCode =
                    CreateRetreatmentNotificationResponseStatusCode.NOTIFICATION_ALREADY_EXISTED,
            };
        }

        //Create new Retreatment Notification
        var newNotification = new RetreatmentNotification()
        {
            Id = Guid.NewGuid(),
            ExaminationDate = command.ExaminationDate,
            PatientId = command.PatientId,
            RetreatmentTypeId = command.RetreatmentTypeId,
        };

        //Handler SMS notification
        var isNotified = await _smsHandler.SendNotification(to: command.To, body: command.Message);

        if (!isNotified)
        {
            return new()
            {
                StatusCode = CreateRetreatmentNotificationResponseStatusCode.SMS_NOTIFICATION_FAIL,
            };
        }

        //Create new Retreatment Notification into database
        var dbResult =
            await _unitOfWork.CreateRetreatmentNotificationRepository.CreateNewNotification(
                notification: newNotification,
                cancellationToken: ct
            );

        //Check if operation failed
        if (!dbResult)
        {
            return new()
            {
                StatusCode =
                    CreateRetreatmentNotificationResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        //Return successful code
        return new()
        {
            StatusCode = CreateRetreatmentNotificationResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
