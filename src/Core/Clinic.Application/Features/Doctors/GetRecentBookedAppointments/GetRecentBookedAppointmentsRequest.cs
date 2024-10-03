using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Application.Features.Doctors.GetRecentBookedAppointments;

/// <summary>
///     GetAppointmentsByDate Request
/// </summary>

public class GetRecentBookedAppointmentsRequest : IFeatureRequest<GetRecentBookedAppointmentsResponse>
{
    public int Size {  get; set; }
}
