using System;
using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Users.SendFeedBack;

public sealed class SendFeedBackRequestValidator
    : FeatureRequestValidator<SendFeedBackRequest, SendFeedBackResponse>
{
    public SendFeedBackRequestValidator()
    {
        RuleFor(expression: request => request.AppointmentId)
            .NotEmpty()
            .Must(predicate: id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("AppointmentId must be a valid");
    }
}
