using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Notification.CreateRetreatmentNotification;

public interface ICreateRetreatmentNotificationRepository
{
    Task<bool> IsExistNotification(
        DateTime examinationDate,
        CancellationToken cancellationToken = default
    );

    Task<bool> CreateNewNotification(
        RetreatmentNotification notification,
        CancellationToken cancellationToken = default
    );
}
