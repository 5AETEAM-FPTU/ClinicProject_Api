using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Schedules.GetDoctorScheduleByDate;

public sealed class GetDoctorScheduleByDateRequest
    : IFeatureRequest<GetDoctorScheduleByDateResponse>
{
    [BindFrom("date")]
    public DateTime Date { get; set; }

    [BindFrom("doctorId")]
    public Guid DoctorId { get; set; }
}
