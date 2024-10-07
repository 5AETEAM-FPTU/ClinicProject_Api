using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentStatus;

public class UpdateAppointmentStatusRequest:IFeatureRequest<UpdateAppointmentStatusResponse>{
    public Guid AppointmentId { get; init; }
    public Guid AppointmentStatusId {get; init;}
}