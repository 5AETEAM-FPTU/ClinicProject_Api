using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.GetUserNotification;

public interface IGetUserNotificationRepository
{
    Task<IEnumerable<RetreatmentNotification>> FindAllNotification(
        Guid userId,
        CancellationToken cancellationToken
    );
}
