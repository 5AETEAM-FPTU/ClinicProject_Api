using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Schedules.GetDoctorMonthlyDate;

public sealed class GetDoctorMonthlyDateRequest : IFeatureRequest<GetDoctorMonthlyDateResponse>
{
    [BindFrom("year")]
    public int Year { get; set; }

    [BindFrom("month")]
    public int Month { get; set; }

    [BindFrom("doctorId")]
    public Guid DoctorId { get; set; }
}
