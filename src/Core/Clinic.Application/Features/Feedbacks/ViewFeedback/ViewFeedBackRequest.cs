using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Feedbacks.ViewFeedback;

public class ViewFeedBackRequest : IFeatureRequest<ViewFeedBackResponse>
{
    //[BindFrom("appointmentId")]
    public Guid AppointmentId { get; set; }
}
