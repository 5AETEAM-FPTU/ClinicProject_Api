using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Notification.CreateRetreatmentNotification;

public class CreateRetreatmentNotificationRequest
    : IFeatureRequest<CreateRetreatmentNotificationResponse>
{
    public DateTime ExaminationDate { get; init; }
    public Guid RetreatmentTypeId { get; init; }
    public Guid PatientId { get; init; }
    public string Message { get; init; }
    public string To { get; init; }
}
