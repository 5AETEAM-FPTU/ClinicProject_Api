﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Appointments.GetRecentAbsent;

/// <summary>
///     GetRecentAbsent Handler
/// </summary>
public class GetRecentAbsentHandler
    : IFeatureHandler<GetRecentAbsentRequest, GetRecentAbsentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetRecentAbsentHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetRecentAbsentResponse> ExecuteAsync(
        GetRecentAbsentRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Check role doctor from role type jwt
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");
        if (!role.Equals("staff"))
        {
            return new() { StatusCode = GetRecentAbsentResponseStatusCode.ROLE_IS_NOT_STAFF };
        }

        // Found appointments booked by userId
        var foundAppointment =
            await _unitOfWork.GetRecentAbsentRepository.FindRecentAbsentQueryAsync(
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetRecentAbsentResponse()
        {
            StatusCode = GetRecentAbsentResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Appointment = foundAppointment.Select(
                    appointment => new GetRecentAbsentResponse.Body.AppointmentDetail()
                    {
                        Id = appointment.Id,
                        FullName = appointment.Patient.User.FullName,
                        AvatarUrl = appointment.Patient.User.Avatar,
                        StartDate = appointment.Schedule.StartDate,
                        EndDate = appointment.Schedule.EndDate,
                    }
                ),
            },
        };
    }
}