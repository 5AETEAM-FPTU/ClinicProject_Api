using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.SendFeedBack;

public class SendFeedBackRequest : IFeatureRequest<SendFeedBackResponse>
{
    public string Comment { get; init; }
    public int Vote { get; init; }
    public Guid AppointmentId { get; init; }
}
