using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Doctors.GetAppointmentsByDate;

/// <summary>
///     GetAppointmentsByDate Handler
/// </summary>
public class GetAppointmentsByDateHandler
    : IFeatureHandler<GetAppointmentsByDateRequest, GetAppointmentsByDateResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAppointmentsByDateHandler(
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
    public async Task<GetAppointmentsByDateResponse> ExecuteAsync(
        GetAppointmentsByDateRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Found user by userId
        var foundUser = await _unitOfWork.GetAppointmentsByDateRepository.GetUserByIdAsync(
            userId,
            cancellationToken
        );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new GetAppointmentsByDateResponse()
            {
                StatusCode = GetAppointmentsByDateResponseStatusCode.USER_IS_NOT_FOUND,
            };
        }

        // Handle date
        var startDate = request.StartDate.Date;
        var endDate = request.EndDate;
        if (request.EndDate != null)
        {
            endDate = request.EndDate?.Date.AddDays(1).AddTicks(-1);
        }
        else
        {
            endDate = startDate.AddDays(1).AddTicks(-1);
        }
        //Get Appointments on day and by doctorId
        var appointments =
            await _unitOfWork.GetAppointmentsByDateRepository.GetAppointmentsByDateQueryAsync(
                startDate: startDate,
                endDate: endDate,
                userId: userId,
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAppointmentsByDateResponse()
        {
            StatusCode = GetAppointmentsByDateResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Appointment = appointments
                    .Select(appointment => new GetAppointmentsByDateResponse.Body.AppointmentDTO()
                    {
                        Id = appointment.Id,
                        Description = appointment.Description,
                        Patient = new GetAppointmentsByDateResponse.Body.AppointmentDTO.PatientDTO()
                        {
                            Avatar = appointment.Patient.User.Avatar,
                            FullName = appointment.Patient.User.FullName,
                            PhoneNumber = appointment.Patient.User.PhoneNumber,
                            Gender =
                                new GetAppointmentsByDateResponse.Body.AppointmentDTO.PatientDTO.GenderDTO()
                                {
                                    Id = appointment.Patient.User.Gender.Id,
                                    Name = appointment.Patient.User.Gender.Name,
                                    Constant = appointment.Patient.User.Gender.Constant,
                                },
                            DOB = appointment.Patient.DOB,
                        },
                        Schedule =
                            new GetAppointmentsByDateResponse.Body.AppointmentDTO.ScheduleDTO()
                            {
                                StartDate = appointment.Schedule.StartDate,
                                EndDate = appointment.Schedule.EndDate,
                            },
                        AppointmentStatus =
                            new GetAppointmentsByDateResponse.Body.AppointmentDTO.AppointmentStatusDTO()
                            {
                                Id = appointment.AppointmentStatus.Id,
                                StatusName = appointment.AppointmentStatus.StatusName,
                                Constant = appointment.AppointmentStatus.Constant,
                            },
                        IsHadMedicalReport = appointment.MedicalReport != null,
                    })
                    .ToList(),
            },
        };
    }
}
