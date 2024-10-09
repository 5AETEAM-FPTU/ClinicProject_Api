using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentStatus;

internal class UpdateAppointmentStatusHandler
    : IFeatureHandler<UpdateAppointmentStatusRequest, UpdateAppointmentStatusResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateAppointmentStatusHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UpdateAppointmentStatusResponse> ExecuteAsync(
        UpdateAppointmentStatusRequest command,
        CancellationToken ct
    )
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        /// <summary>
        /// User are not allow
        /// </summary>
        /// <returns></returns>
        if (Equals(objA: role, objB: "user"))
        {
            return new() { StatusCode = UpdateAppointmentStatusResponseStatusCode.FORBIDEN_ACCESS };
        }
        // Check if appointment is existed
        var foundAppointment =
            await _unitOfWork.UpdateAppointmentStatusRepository.IsAppointmentExistedByIdAsync(
                command.AppointmentId,
                ct
            );

        if (Equals(objA: foundAppointment, objB: default))
        {
            return new()
            {
                StatusCode = UpdateAppointmentStatusResponseStatusCode.APPOINTMENT_IS_NOT_FOUND,
            };
        }
        // Check if status is existed
        var foundStatus =
            await _unitOfWork.UpdateAppointmentStatusRepository.IsStatusExistedByIdAsync(
                command.AppointmentStatusId,
                ct
            );

        if (Equals(objA: foundStatus, objB: default))
        {
            return new()
            {
                StatusCode = UpdateAppointmentStatusResponseStatusCode.STATUS_IS_NOT_ACCEPTABLE,
            };
        }
        // Update
        var dbResult =
            await _unitOfWork.UpdateAppointmentStatusRepository.UpdateAppointmentStatusCommandAsync(
                command.AppointmentId,
                command.AppointmentStatusId,
                ct
            );

        if (Equals(objA: dbResult, objB: default))
        {
            return new()
            {
                StatusCode = UpdateAppointmentStatusResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        return new() { StatusCode = UpdateAppointmentStatusResponseStatusCode.OPERATION_SUCCESS };
    }
}
