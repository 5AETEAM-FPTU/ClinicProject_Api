using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Application.Features.Doctors.GetAppointmentsByDate;

/// <summary>
///     GetAppointmentsByDate Request
/// </summary>

public class GetAppointmentsByDateRequest : IFeatureRequest<GetAppointmentsByDateResponse>
{
    [BindFrom("startDate")]
    public DateTime StartDate { get; set; }

    [BindFrom("endDate")]
    public DateTime? EndDate { get; set; }
}
