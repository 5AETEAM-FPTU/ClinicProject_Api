
using Clinic.Domain.Commons.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Feedbacks.ViewFeedback;

public interface IViewFeedbackRepository
{
    Task<Feedback> GetFeedBackQueryAsync(Guid appointmentId, CancellationToken cancellationToken);
    Task<double> GetRatingQueryAsync(Guid doctorId, CancellationToken cancellationToken);
    Task<bool> IsExistFeedback(Guid appointmentId, CancellationToken cancellationToken);
    Task<User> GetDoctorByIdQueryAsync(Guid appointmentId, CancellationToken cancellationToken);
    Task<Appointment> GetAppointmentByIdQueryAsync(Guid appointmentId, CancellationToken cancellationToken);
}
