using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.Schedules.GetSchedulesByDate;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Application.Features.Schedules.GetDoctorScheduleByDate;

internal sealed class GetDoctorScheduleByDateHandler
    : IFeatureHandler<GetDoctorScheduleByDateRequest, GetDoctorScheduleByDateResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDoctorScheduleByDateHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDoctorScheduleByDateResponse> ExecuteAsync(
        GetDoctorScheduleByDateRequest command,
        CancellationToken ct
    )
    {
        var foundDoctor = await _unitOfWork.GetDoctorScheduleByDateRepository.GetUserByDoctorId(
            command.DoctorId,
            ct
        );

        if (Equals(foundDoctor, default))
        {
            return new()
            {
                StatusCode = GetDoctorScheduleByDateResponseStatusCode.DOCTOR_IS_NOT_FOUND,
            };
        }

        var startDate = command.Date.Date;
        var endDate = startDate.AddDays(1).AddTicks(-1);

        var schedules = await _unitOfWork.GetSchedulesByDateRepository.GetSchedulesByDateQueryAsync(
            startDate: startDate,
            endDate: endDate,
            doctorId: command.DoctorId,
            cancellationToken: ct
        );

        return new()
        {
            StatusCode = GetDoctorScheduleByDateResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                TimeSlots = schedules
                    .Select(schedule => new GetDoctorScheduleByDateResponse.Body.TimeSlot()
                    {
                        SlotId = schedule.Id,
                        StartTime = schedule.StartDate,
                        EndTime = schedule.EndDate,
                        IsHadAppointment = schedule.Appointment != null,
                    })
                    .ToList(),
            },
        };
    }
}
