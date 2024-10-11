using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.OnlinePayments.CreatePaymentLink;

/// <summary>
///     CreatePaymentLink Request
/// </summary>
public class CreatePaymentLinkRequest : IFeatureRequest<CreatePaymentLinkResponse>
{
    public string Description { get; set; } = string.Empty;
    public int Amount { get; set; }
    public Guid AppointmentId { get; set; }
}
