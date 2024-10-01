using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.OnlinePayments.CreateNewOnlinePayment;

public sealed class CreateNewOnlinePaymentRequestValidator: FeatureRequestValidator<CreateNewOnlinePaymentRequest, CreateNewOnlinePaymentResponse> {
    public CreateNewOnlinePaymentRequestValidator() { 
        RuleFor(expression: request => request.Amount)
            .GreaterThan(0).NotEmpty();
        RuleFor(expression: request => request.AppointmentId)
        .Must(predicate: id => Guid.TryParse(id.ToString(), out _))
        .WithMessage("AppointmentId must be a valid");
        RuleFor(expression: request => request.PaymentMethod).NotEmpty();
        RuleFor(expression: request => request.TransactionId).NotEmpty();

    }
 }
