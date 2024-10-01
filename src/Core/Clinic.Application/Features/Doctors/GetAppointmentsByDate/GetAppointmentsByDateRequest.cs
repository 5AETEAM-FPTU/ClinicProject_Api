using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Doctors.GetAppointmentsByDate;

/// <summary>
///     GetAppointmentsByDate Request
/// </summary>

public class GetAppointmentsByDateRequest : IFeatureRequest<GetAppointmentsByDateResponse>
{
    [BindFrom("date")]
    public DateTime Date { get; set; }
}
