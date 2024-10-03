using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.Doctors.GetRecentBookedAppointments;
using FluentValidation;

namespace Clinic.Application.Features.Doctors.GetAppointmentsByDate;

/// <summary>
///     GetSchedulesByDate request validator.
/// </summary>
public sealed class GetRecentBookedAppointmentsRequestValidator
    : FeatureRequestValidator<GetRecentBookedAppointmentsRequest, GetRecentBookedAppointmentsResponse>
{
    public GetRecentBookedAppointmentsRequestValidator()
    {
        RuleFor(expression: request => request.Size).NotEmpty();
    }
}
