using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Appointments.GetAbsentForStaff;

/// <summary>
///     GetAbsentForStaff Handler
/// </summary>
public class GetAbsentForStaffHandler
    : IFeatureHandler<GetAbsentForStaffRequest, GetAbsentForStaffResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetAbsentForStaffHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
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
    public async Task<GetAbsentForStaffResponse> ExecuteAsync(
        GetAbsentForStaffRequest request,
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
            return new() { StatusCode = GetAbsentForStaffResponseStatusCode.ROLE_IS_NOT_DOCTOR };
        }

        // Found appointments booked by userId
        var foundAppointment =
            await _unitOfWork.GetAbsentForStaffRepository.GetAbsentForStaffByUserIdQueryAsync(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken
            );
        var countAppointments =
            await _unitOfWork.GetAbsentForStaffRepository.CountAllAbsentForStaffQueryAsync(
                cancellationToken
            );
        // Response successfully.
        return new GetAbsentForStaffResponse()
        {
            StatusCode = GetAbsentForStaffResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Appointment =
                    new PaginationResponse<GetAbsentForStaffResponse.Body.AppointmentDetail>()
                    {
                        Contents = foundAppointment.Select(
                            appointment => new GetAbsentForStaffResponse.Body.AppointmentDetail()
                            {
                                Id = appointment.Id,
                                Description = appointment.Description,
                                Patients =
                                    new GetAbsentForStaffResponse.Body.AppointmentDetail.UserDetail()
                                    {
                                        UserId = appointment.Patient.UserId,
                                        FullName = appointment.Patient.User.FullName,
                                        AvatarUrl = appointment.Patient.User.Avatar,
                                        DOB = appointment.Patient.DOB,
                                        Gender = appointment.Patient.User.Gender.Constant,
                                        PhoneNumber = appointment.Patient.User.PhoneNumber,
                                    },
                                Schedules =
                                    new GetAbsentForStaffResponse.Body.AppointmentDetail.Schedule()
                                    {
                                        ScheduleId = appointment.Schedule.Id,
                                        StartDate = appointment.Schedule.StartDate,
                                        EndDate = appointment.Schedule.EndDate,
                                    },
                                AppointmentStatus =
                                    new GetAbsentForStaffResponse.Body.AppointmentDetail.Status()
                                    {
                                        Id = appointment.AppointmentStatus.Id,
                                        StatusName = appointment.AppointmentStatus.StatusName,
                                        Constant = appointment.AppointmentStatus.Constant,
                                    },
                            }
                        ),
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                        TotalPages = (int)
                            Math.Ceiling((double)countAppointments / request.PageSize),
                    },
            },
        };
    }
}
