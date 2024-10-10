using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.OnlinePayments.CreatePaymentLink;

/// <summary>
///     CreatePaymentLink Request Validator.
/// </summary>
public sealed class CreatePaymentLinkRequestValidator
    : FeatureRequestValidator<CreatePaymentLinkRequest, CreatePaymentLinkResponse>
{
    public CreatePaymentLinkRequestValidator()
    {
        RuleFor(expression: request => request.Amount).GreaterThan(0).NotEmpty();

        RuleFor(expression: request => request.AppointmentId).NotEmpty();
    }
}
