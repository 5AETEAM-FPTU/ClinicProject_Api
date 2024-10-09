using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Appointments.GetAbsentAppointment;

/// <summary>
///     GetAbsentAppointment Handler
/// </summary>
public class GetAbsentAppointmentHandler
    : IFeatureHandler<GetAbsentAppointmentRequest, GetAbsentAppointmentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAbsentAppointmentHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetAbsentAppointmentResponse> ExecuteAsync(
        GetAbsentAppointmentRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Check role doctor from role type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("doctor"))
        {
            return new() { StatusCode = GetAbsentAppointmentResponseStatusCode.ROLE_IS_NOT_DOCTOR };
        }

        // Found appointments booked by userId
        var foundAppointment =
            await _unitOfWork.GetAbsentAppointmentRepository.GetAbsentAppointmentByUserIdQueryAsync(
                userId: userId,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAbsentAppointmentResponse()
        {
            StatusCode = GetAbsentAppointmentResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Appointment = foundAppointment.Select(
                    appointment => new GetAbsentAppointmentResponse.Body.AppointmentDetail()
                    {
                        Id = appointment.Id,
                        Description = appointment.Description,
                        Patients =
                            new GetAbsentAppointmentResponse.Body.AppointmentDetail.UserDetail()
                            {
                                UserId = appointment.Patient.UserId,
                                FullName = appointment.Patient.User.FullName,
                                AvatarUrl = appointment.Patient.User.Avatar,
                                DOB = appointment.Patient.DOB,
                                Gender = appointment.Patient.User.Gender.Constant,
                                PhoneNumber = appointment.Patient.User.PhoneNumber,
                            },
                        Schedules =
                            new GetAbsentAppointmentResponse.Body.AppointmentDetail.Schedule()
                            {
                                ScheduleId = appointment.Schedule.Id,
                                StartDate = appointment.Schedule.StartDate,
                                EndDate = appointment.Schedule.EndDate,
                            },
                        AppointmentStatus =
                            new GetAbsentAppointmentResponse.Body.AppointmentDetail.Status()
                            {
                                Id = appointment.AppointmentStatus.Id,
                                StatusName = appointment.AppointmentStatus.StatusName,
                                Constant = appointment.AppointmentStatus.Constant,
                            },
                    }
                ),
            },
        };
    }
}
