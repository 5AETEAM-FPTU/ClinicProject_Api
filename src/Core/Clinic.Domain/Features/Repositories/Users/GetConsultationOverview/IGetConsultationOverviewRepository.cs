using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Clinic.Domain.Features.Repositories.Users.GetConsultationOverview;

/// <summary>
///     Interface for Query GetConsultationOverview Repository
/// </summary>
public interface IGetConsultationOverviewRepository
{
    Task<IEnumerable<QueueRoom>> FindAllPendingConsultationByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
    Task<IEnumerable<ChatRoom>> FindAllDoneConsultationByUserIdQueryAsync(
      Guid userId,
      CancellationToken cancellationToken
  );
}
