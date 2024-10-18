using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.GetUserNotification;

/// <summary>
///     GetUserNotification Response
/// </summary>
public class GetUserNotificationResponse : IFeatureResponse
{
    public GetUserNotificationResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<Notification> RetreatmentNotifications { get; init; }

        public sealed class Notification
        {
            public Guid NotificationId { get; init; }
            public DateTime ExaminationDate { get; init; }
            public RetreatmentType Type { get; init; }

            public sealed class RetreatmentType
            {
                public Guid TypeId { get; init; }
                public string TypeName { get; init; }
                public string TypeConstant { get; init; }
            }
        }
    }
}
