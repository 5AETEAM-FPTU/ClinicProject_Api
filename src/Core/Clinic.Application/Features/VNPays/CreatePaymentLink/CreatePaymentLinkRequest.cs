using Clinic.Application.Commons.Abstractions;
using System;

namespace Clinic.Application.Features.Users.VNPays.CreatePaymentLink;

/// <summary>
///     GetAllDoctors Request
/// </summary>
public class CreatePaymentLinkRequest : IFeatureRequest<CreatePaymentLinkResponse>
{
    public string Description { get; set; } = string.Empty;
    public int Amount { get; set; }
    public Guid SlotId { get; set; }    

}
