using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Appointments.UpdateUserBookedAppointment;

public class UpdateUserBookedAppointmentHandler : IFeatureHandler<UpdateUserBookedAppointmentRequest, UpdateUserBookedAppointmentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    public UpdateUserBookedAppointmentHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async Task<UpdateUserBookedAppointmentResponse> ExecuteAsync(UpdateUserBookedAppointmentRequest command, CancellationToken ct)
    {
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if (!Equals(objA: role, objB: "user"))
        {
            return new() { StatusCode = UpdateUserBookedAppointmentResponseStatusCode.FORBIDEN_ACCESS };
        }

        var foundAppointment = await _unitOfWork.UpdateUserBookedAppointmentRepository.GetAppointmentByIdAsync(command.AppointmentId, ct);
        if (Equals(objA: foundAppointment, objB: default))
        {
            return new() { StatusCode = UpdateUserBookedAppointmentResponseStatusCode.APPOINTMENTS_IS_NOT_FOUND };
        }

        if (DateTime.Compare(foundAppointment.Schedule.StartDate, DateTime.Now.AddHours(12)) <= 0)
        {
            return new() { StatusCode = UpdateUserBookedAppointmentResponseStatusCode.UPDATE_EXPIRED };
        }

        if (!Equals(objA: Commons.Constance.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID, objB: foundAppointment.UpdatedBy))
        {
            return new() { StatusCode = UpdateUserBookedAppointmentResponseStatusCode.APPOINTMENT_ONLY_UPDATE_ONCE };
        }

        // check if user is owner of appointment or not
        var userId = Guid.Parse(_contextAccessor.HttpContext.User.FindFirstValue(claimType: "sub"));
        if (!Equals(objA: foundAppointment.PatientId, objB: userId))
        {
            return new() { StatusCode = UpdateUserBookedAppointmentResponseStatusCode.FORBIDEN_ACCESS };
        }
        

        var dbResult = await _unitOfWork.UpdateUserBookedAppointmentRepository.UpdateUserBookedAppointmentCommandAsync(foundAppointment.Id, userId, command.SelectedSlotID, ct);

        if (!dbResult)
        {
            return new()
            {
                StatusCode = UpdateUserBookedAppointmentResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = UpdateUserBookedAppointmentResponseStatusCode.OPERATION_SUCCESS
        };
    }
}
