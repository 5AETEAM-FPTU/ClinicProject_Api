using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Appointments.UpdateAppointmentStatus;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentStatus;

internal class UpdateAppointmentStatusRepository : IUpdateAppointmentStatusRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Appointment> _appointment;
    private readonly DbSet<AppointmentStatus> _appointmentStatus;

    public UpdateAppointmentStatusRepository(ClinicContext context)
    {
        _context = context;
        _appointment = _context.Set<Appointment>();
        _appointmentStatus = _context.Set<AppointmentStatus>();
    }

    public Task<bool> IsAppointmentExistedByIdAsync(Guid appointmentId, CancellationToken ct)
    {
        return _appointment.AnyAsync(appointment => appointment.Id == appointmentId, ct);
    }

    public Task<bool> IsStatusExistedByIdAsync(Guid statusId, CancellationToken ct)
    {
        return _appointmentStatus.AnyAsync(status => status.Id == statusId, ct);
    }

    public async Task<bool> UpdateAppointmentStatusCommandAsync(
        Guid AppointmentId,
        Guid AppointmentStatusId,
        CancellationToken ct
    )
    {
        var dbResult = false;
        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync(ct);

                try
                {
                    await _appointment
                        .Where(predicate: entity => entity.Id == AppointmentId)
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder.SetProperty(entity => entity.StatusId, AppointmentStatusId)
                        );
                    await transaction.CommitAsync(ct);
                    dbResult = true;
                }
                catch
                {
                    await transaction.RollbackAsync(ct);
                }
            });
        return dbResult;
    }
}
