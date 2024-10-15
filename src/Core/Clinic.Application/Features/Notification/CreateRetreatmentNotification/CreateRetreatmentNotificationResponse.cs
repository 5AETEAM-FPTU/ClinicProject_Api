using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Notification.CreateRetreatmentNotification;

/// <summary>
///     CreateRetreatmentNotificationResponse
/// </summary>
public sealed class CreateRetreatmentNotificationResponse : IFeatureResponse
{
    public CreateRetreatmentNotificationResponseStatusCode StatusCode { get; set; }

}

