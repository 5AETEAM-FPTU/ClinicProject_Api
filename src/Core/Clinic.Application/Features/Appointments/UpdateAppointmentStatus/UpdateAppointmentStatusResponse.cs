using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentStatus;

/// <summary>
/// UpdateAppointmentStatusResponse
/// </summary>
public class UpdateAppointmentStatusResponse : IFeatureResponse
{
    public UpdateAppointmentStatusResponseStatusCode StatusCode { get; init; }
}
