using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Appointments.RemoveAppointment;
using Clinic.MySQL.Data.Context;
using Clinic.MySQL.Data.DataSeeding;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.ChatRooms.RemoveAppointment;

/// <summary>
///     Implementation of IRemoveAppointmentRepository.
/// </summary>
internal class RemoveAppointmentRepository : IRemoveAppointmentRepository
{
    private readonly ClinicContext _context;
    private DbSet<Appointment> _appointments;

    public RemoveAppointmentRepository(ClinicContext context)
    {
        _context = context;
        _appointments = _context.Set<Appointment>();
    }

    public async Task<bool> DeleteAppointment(CancellationToken cancellationToken = default)
    {
        var dbTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken
                );
                try
                {
                    await _appointments
                        .Where(predicate: entity =>
                            entity.StatusId == EnumConstant.AppointmentStatus.PENDING
                            && entity.CreatedAt.AddMinutes(5) < DateTime.Now
                        )
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await transaction.CommitAsync(cancellationToken: cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return dbTransactionResult;
    }
}
