
using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Feedbacks.DoctorGetAllFeedbacks;

public interface IDoctorGetAllFeedbacksRepository
{
    Task<int> CountAllFeedbacksQueryAsync(Guid userId, int? vote, CancellationToken cancellationToken);
    Task<IEnumerable<Feedback>> GetAllFeedbacksQueryAsync(int pageIndex, int pageSize, Guid userId, int? vote, CancellationToken cancellationToken);
    Task<double> GetRatingQueryAsync(Guid userId, CancellationToken cancellationToken);

}
