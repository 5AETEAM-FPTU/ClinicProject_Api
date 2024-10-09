using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentStatus;

/// <summary>
/// Request for updating appointment status
/// </summary>
public class UpdateAppointmentStatusRequest : IFeatureRequest<UpdateAppointmentStatusResponse>
{
    public Guid AppointmentId { get; init; }
    public Guid AppointmentStatusId { get; init; }
}
