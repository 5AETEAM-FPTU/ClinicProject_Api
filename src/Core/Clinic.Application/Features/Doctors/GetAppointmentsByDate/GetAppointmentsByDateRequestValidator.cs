using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Doctors.GetAppointmentsByDate;

/// <summary>
///     GetSchedulesByDate request validator.
/// </summary>
public sealed class GetAppointmentsByDateRequestValidator
    : FeatureRequestValidator<GetAppointmentsByDateRequest, GetAppointmentsByDateResponse>
{
    public GetAppointmentsByDateRequestValidator()
    {
        RuleFor(expression: request => request.StartDate).NotEmpty();
    }
}
