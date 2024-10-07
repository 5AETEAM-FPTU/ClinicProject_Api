using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Appointments.UpdateUserBookedAppointment;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Appointments.UpdateUserBookedAppointment
{
    public class UpdateUserBookedAppointmentRepository : IUpdateUserBookedAppointmentRepository
    {
        private ClinicContext _context;
        private DbSet<Appointment> _appointments;
        public UpdateUserBookedAppointmentRepository(ClinicContext context)
        {
            _context = context;
            _appointments = _context.Set<Appointment>();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(Guid id, CancellationToken ct)
        {
            return await _appointments
           .Include(entity => entity.Schedule)
           .Where(entity => entity.Id == id)
           .FirstOrDefaultAsync(cancellationToken: ct);
        }

        public async Task<bool> UpdateUserBookedAppointmentCommandAsync(Guid appointmentId, Guid userId, Guid slotId, CancellationToken ct)
        {
            var dbResult = false;

            await _context
                .Database.CreateExecutionStrategy()
                .ExecuteAsync(operation: async () =>
                {
                    using var transaction = await _context.Database.BeginTransactionAsync(ct);

                    try
                    {
                        await _appointments
                            .Where(predicate: entity => entity.Id == appointmentId)
                            .ExecuteUpdateAsync(setPropertyCalls: builder =>
                                builder
                                .SetProperty(entity => entity.ScheduleId, slotId)
                                .SetProperty(entity => entity.UpdatedAt, DateTime.Now)
                                .SetProperty(entity => entity.UpdatedBy, userId)
                            );

                        await transaction.CommitAsync(ct);
                        dbResult = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        await transaction.RollbackAsync(ct);
                    }
                });
            return dbResult;
        }
    }
}
