using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Doctors.GetRecentBookedAppointments;

public interface IGetRecentBookedAppointmentsRepository
{
    Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);

    Task<IEnumerable<Appointment>> GetRecentBookedAppointmentsByDoctorIdQueryAsync(Guid userId,int size, CancellationToken cancellationToken);
}