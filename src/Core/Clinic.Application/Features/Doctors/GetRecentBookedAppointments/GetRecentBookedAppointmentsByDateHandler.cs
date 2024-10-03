using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Doctors.GetRecentBookedAppointments;

/// <summary>
///     GetAppointmentsByDate Handler
/// </summary>
public class GetRecentBookedAppointmentsByDateHandler
    : IFeatureHandler<GetRecentBookedAppointmentsRequest, GetRecentBookedAppointmentsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetRecentBookedAppointmentsByDateHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetRecentBookedAppointmentsResponse> ExecuteAsync(
        GetRecentBookedAppointmentsRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Found user by userId
        var foundUser =
            await _unitOfWork.GetAppointmentsByDateRepository.GetUserByIdAsync(
                    userId,
                    cancellationToken
                );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new GetRecentBookedAppointmentsResponse()
            {
                StatusCode = GetRecentBookedAppointmentsResponseStatusCode.USER_IS_NOT_FOUND
            };
        }

        //Get recent booked appointments by doctorId
        var appointments = await _unitOfWork.GetRecentBookedAppointmentsRepository.GetRecentBookedAppointmentsByDoctorIdQueryAsync(
            userId: userId,
            size: request.Size,
            cancellationToken: cancellationToken
        );



        // Response successfully.
        return new GetRecentBookedAppointmentsResponse()
        {
            StatusCode = GetRecentBookedAppointmentsResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Appointments = appointments.Select(appointment => new GetRecentBookedAppointmentsResponse.Body.AppointmentDTO()
                {
                    Id = appointment.Id,
                    Patient = new GetRecentBookedAppointmentsResponse.Body.AppointmentDTO.PatientDTO()
                    {
                        Avatar = appointment.Patient.User.Avatar,
                        FullName = appointment.Patient.User.FullName
                    },
                    Schedule = new GetRecentBookedAppointmentsResponse.Body.AppointmentDTO.ScheduleDTO()
                    {
                        StartDate = appointment.Schedule.StartDate,
                        EndDate = appointment.Schedule.EndDate
                    },
                    CreatedAt = appointment.CreatedAt
                }
                )
                .ToList()
            }

        };
    }
}
