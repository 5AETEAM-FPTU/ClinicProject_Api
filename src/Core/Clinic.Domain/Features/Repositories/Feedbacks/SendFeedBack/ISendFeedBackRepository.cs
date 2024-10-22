using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Feedbacks.SendFeedBack;

public interface ISendFeedBackRepository
{
    Task<bool> IsExistFeedBack(Guid appointmentId, CancellationToken cancellationToken = default);

    Task<bool> CreateNewFeedBack(Feedback feedback, CancellationToken cancellationToken = default);

    Task<bool> IsAppointmentDone(Guid appointmentId, CancellationToken cancellationToken = default);
}
