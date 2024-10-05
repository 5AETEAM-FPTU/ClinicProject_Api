﻿using System;
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
            return new()
            {
                StatusCode = GetAbsentAppointmentResponseStatusCode.ROLE_IS_NOT_DOCTOR,
            };
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
                        AppointmentId = appointment.Id,
                        ScheduleId = appointment.Schedule.Id,
                        StartDate = appointment.Schedule.StartDate,
                        EndDate = appointment.Schedule.EndDate,
                        Users = new GetAbsentAppointmentResponse.Body.AppointmentDetail.UserDetail()
                        {
                            UserId = appointment.Schedule.Doctor.UserId,
                            FullName = appointment.Schedule.Doctor.User.FullName,
                            AvatarUrl = appointment.Schedule.Doctor.User.Avatar,
                            DOB = appointment.Patient.DOB,
                            Description = appointment.Patient.Description,
                            Gender = appointment.Patient.User.Gender.Constant,
                        }
                    }
                )
            },
        };
    }
}