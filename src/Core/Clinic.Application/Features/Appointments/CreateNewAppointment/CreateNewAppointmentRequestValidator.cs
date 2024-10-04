using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Appointments.CreateNewAppointment;

public sealed class CreateNewAppointmentRequestValidator
    : FeatureRequestValidator<CreateNewAppointmentRequest, CreateNewAppointmentResponse>
{
    public CreateNewAppointmentRequestValidator()
    {
        RuleFor(expression: request => request.ScheduleId)
            .NotEmpty()
            .Must(predicate: id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("ScheduleId must be a valid");
        RuleFor(expression: request => request.Description).NotEmpty();
        RuleFor(expression: request => request.DepositPayment).NotEmpty();
        RuleFor(expression: request => request.ReExamination).NotEmpty();
    }
}
