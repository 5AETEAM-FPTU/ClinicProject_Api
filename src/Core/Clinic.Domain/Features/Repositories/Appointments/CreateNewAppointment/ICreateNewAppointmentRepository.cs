using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Appointments.CreateNewAppointment;

public interface ICreateNewAppointmentRepository {
    Task<AppointmentStatus> GetPendingStatusAsync(CancellationToken cancellationToken = default);

    Task<bool> IsExistSchedule(Guid schedueleId, CancellationToken cancellationToken = default);

    Task<bool> IsExistScheduleHadAppointment(Guid schedueleId, CancellationToken cancellationToken = default);

    Task<bool> CreateNewAppointment(Appointment appointment, CancellationToken cancellationToken = default);

    Task<Schedule> GetScheduleByIdAsync(Guid scheduleId, CancellationToken cancellationToken = default);
}
