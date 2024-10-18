using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.GetUserNotification;

/// <summary>
///     UserDetailAndRecentMedicalReport Request
/// </summary>
public class GetUserNotificationRequest
    : IFeatureRequest<GetUserNotificationResponse>
{
    public Guid UserId { get; init; }
}
