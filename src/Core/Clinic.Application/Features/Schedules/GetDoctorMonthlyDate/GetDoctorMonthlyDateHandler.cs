using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Features.UnitOfWorks;

namespace Clinic.Application.Features.Schedules.GetDoctorMonthlyDate;

internal class GetDoctorMonthlyDateHandler
    : IFeatureHandler<GetDoctorMonthlyDateRequest, GetDoctorMonthlyDateResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDoctorMonthlyDateHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDoctorMonthlyDateResponse> ExecuteAsync(
        GetDoctorMonthlyDateRequest command,
        CancellationToken ct
    )
    {
        var foundDoctor = await _unitOfWork.GetDoctorMonthlyDateRepository.GetUserByDoctorId(
            command.DoctorId,
            ct
        );
        if (Equals(foundDoctor, default))
        {
            return new()
            {
                StatusCode = GetDoctorMonthlyDateResponseStatusCode.DOCTOR_IS_NOT_FOUND,
            };
        }
        var schedules =
            await _unitOfWork.GetDoctorMonthlyDateRepository.GetScheduleDatesByMonthQueryAsync(
                year: command.Year,
                month: command.Month,
                doctorId: command.DoctorId,
                cancellationToken: ct
            );
        if (Equals(schedules, default))
        {
            return new()
            {
                StatusCode = GetDoctorMonthlyDateResponseStatusCode.DATABASE_OPERATION_FAILED,
            };
        }
        return new()
        {
            StatusCode = GetDoctorMonthlyDateResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetDoctorMonthlyDateResponse.Body()
            {
                WorkingDays = schedules.Select(s => s.StartDate.Date).Distinct().ToList(),
            },
        };
    }
}
