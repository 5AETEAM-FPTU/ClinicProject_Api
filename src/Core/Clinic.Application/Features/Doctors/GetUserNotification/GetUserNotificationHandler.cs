using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Doctors.GetUserNotification;

/// <summary>
///     GetUserNotification Handler
/// </summary>
public class GetUserNotificationHandler
    : IFeatureHandler<
        GetUserNotificationRequest,
        GetUserNotificationResponse
    >
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetUserNotificationHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor contextAccessor
    )
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
    public async Task<GetUserNotificationResponse> ExecuteAsync(
        GetUserNotificationRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check role "Only user can access"
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!(role.Equals("doctor") || role.Equals("staff")))
        {
            return new GetUserNotificationResponse()
            {
                StatusCode =
                    GetUserNotificationResponseStatusCode.ROLE_IS_NOT_DOCTOR_STAFF,
            };
        }

        // get retreatment notifications
        var retreatments =
            await _unitOfWork.GetUserNotificationRepository.FindAllNotification(
                request.UserId,
                cancellationToken
            );

        //// respond if user not found
        //if (Equals(user, default))
        //{
        //    return new UserDetailAndRecentMedicalReportResponse()
        //    {
        //        StatusCode = UserDetailAndRecentMedicalReportResponseStatusCode.USER_ID_NOT_FOUND,
        //    };
        //}

        // Response successfully.
        return new GetUserNotificationResponse()
        {
            StatusCode = GetUserNotificationResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetUserNotificationResponse.Body()
            {
                RetreatmentNotifications = retreatments.Select(
                    retreatment => new GetUserNotificationResponse.Body.Notification()
                    {
                        ExaminationDate = retreatment.ExaminationDate,
                        NotificationId = retreatment.Id,
                        Type =
                            new GetUserNotificationResponse.Body.Notification.RetreatmentType()
                            {
                                TypeId = retreatment.RetreatmentType.Id,
                                TypeConstant = retreatment.RetreatmentType.Constant,
                                TypeName = retreatment.RetreatmentType.Name,
                            },
                    }
                ),
            },
        };
    }
}
