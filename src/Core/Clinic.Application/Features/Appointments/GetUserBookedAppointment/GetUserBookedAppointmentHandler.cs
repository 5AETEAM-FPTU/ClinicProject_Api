using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Security.Claims;
using System.Linq;

namespace Clinic.Application.Features.Appointments.GetUserBookedAppointment;

/// <summary>
///     GetUserBookedAppointment Handler
/// </summary>
public class GetUserBookedAppointmentHandler
    : IFeatureHandler<GetUserBookedAppointmentRequest, GetUserBookedAppointmentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetUserBookedAppointmentHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetUserBookedAppointmentResponse> ExecuteAsync(
        GetUserBookedAppointmentRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Check role doctor from role type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("user"))
        {
            return new()
            {
                StatusCode = GetUserBookedAppointmentResponseStatusCode.ROLE_IS_NOT_USER,
            };
        }

        // Found appointments booked by userId
        var foundAppointment = await _unitOfWork.GetUserBookedAppointmentRepository.GetUserBookedAppointmentByUserIdQueryAsync(
            userId: userId,
            cancellationToken: cancellationToken
        );

        // Responds if userId is not found
        if (Equals(objA: foundAppointment, objB: default))
        {
            return new GetUserBookedAppointmentResponse()
            {
                StatusCode = GetUserBookedAppointmentResponseStatusCode.APPOINTMENTS_IS_NOT_FOUND,
            };
        }

        // Response successfully.
        return new GetUserBookedAppointmentResponse()
        {
            StatusCode = GetUserBookedAppointmentResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Appointment = foundAppointment.Select(appointment => new GetUserBookedAppointmentResponse.Body.AppointmentDetail()
                {
                    AppointmentId = appointment.Id,
                    ScheduleId = appointment.Schedule.Id,
                    StartDate = appointment.Schedule.StartDate,
                    EndDate = appointment.Schedule.EndDate,
                    DoctorDetails = new GetUserBookedAppointmentResponse.Body.AppointmentDetail.Doctor()
                    {
                        DoctorId = appointment.Schedule.Doctor.UserId,
                        FullName = appointment.Schedule.Doctor.User.FullName,
                        AvatarUrl = appointment.Schedule.Doctor.User.Avatar,
                    } 
                })
            },
        };
    }
}
