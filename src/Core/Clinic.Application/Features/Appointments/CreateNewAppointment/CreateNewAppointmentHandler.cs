using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Appointments.CreateNewAppointment;

internal sealed class CreateNewAppointmentHandler
    : IFeatureHandler<CreateNewAppointmentRequest, CreateNewAppointmentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor; 

    public CreateNewAppointmentHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor contextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    /// <summary>
    /// Empty implementation.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="ct"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///  A task containing the response.
    /// </returns>

    public async Task<CreateNewAppointmentResponse> ExecuteAsync(
        CreateNewAppointmentRequest command,
        CancellationToken ct
    ) { 
        
        var role = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "role");

        if(!Equals(objA: role, objB: "user")) {
            return new() {
                StatusCode = CreateNewAppointmentResponseStatusCode.FORBIDEN_ACCESS
            };
        } 

        var userId = _contextAccessor.HttpContext.User.FindFirstValue(claimType: "sub");

        if(Equals(objA: userId, objB: null)) {
            return new() {
                StatusCode = CreateNewAppointmentResponseStatusCode.USER_IS_NOT_FOUND
            };
        }
         
        var appointmentStatus = await _unitOfWork.CreateNewAppointmentRepository.GetPendingStatusAsync(cancellationToken:ct);

        if(Equals(objA: appointmentStatus, objB: default)) {
            return new() {
                StatusCode = CreateNewAppointmentResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        var foundSchedule = await _unitOfWork.CreateNewAppointmentRepository.IsExistSchedule(command.ScheduleId, cancellationToken:ct);

        if(Equals(objA: foundSchedule, objB: default)) {
            return new() {
                StatusCode = CreateNewAppointmentResponseStatusCode.SCHEDUELE_IS_NOT_FOUND
            };
        }

        var IsExistScheduleHadAppointment = await _unitOfWork.CreateNewAppointmentRepository.IsExistScheduleHadAppointment(command.ScheduleId, cancellationToken:ct);

        if(IsExistScheduleHadAppointment) {
            return new() {
                StatusCode = CreateNewAppointmentResponseStatusCode.SCHEDUELE_IS_NOT_AVAILABLE
            };
        }

        var newAppointment = new Appointment
        {
            Id = Guid.NewGuid(),
            PatientId = command.PatientID,
            ScheduleId = command.ScheduleId,
            StatusId = appointmentStatus.Id,
            DepositPayment = command.DepositPayment,
            ExaminationDate = command.ExaminationDate,
            Description = command.Description,
            CreatedBy = command.PatientID,
            CreatedAt = DateTime.UtcNow,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
        };        

        var dbResult = await _unitOfWork.CreateNewAppointmentRepository.CreateNewAppointment(
            appointment: newAppointment,
            cancellationToken: ct
        );

        if(!dbResult) {
            return new() {
                StatusCode = CreateNewAppointmentResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new CreateNewAppointmentResponse() {
            StatusCode = CreateNewAppointmentResponseStatusCode.OPERATION_SUCCESS
        };
     }
}
