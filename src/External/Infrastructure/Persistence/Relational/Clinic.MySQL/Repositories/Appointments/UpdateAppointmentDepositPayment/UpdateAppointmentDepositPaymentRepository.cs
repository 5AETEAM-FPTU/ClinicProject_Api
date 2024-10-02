using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Appointments.UpdateAppointmentDepositPayment;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;

internal sealed class UpdateAppointmentDepositPaymentRepository
    : IUpdateAppointmentDepositPaymentRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Appointment> _appointments;

    public UpdateAppointmentDepositPaymentRepository(ClinicContext context)
    {
        _context = context;
        _appointments = _context.Set<Appointment>();
    }

    public async Task<Appointment> GetAppointmentByIdAsync(Guid id, CancellationToken ct)
    {
        return await _appointments
            .Where(entity => entity.Id == id)
            .Select(entity => new Appointment() { Id = entity.Id })
            .FirstOrDefaultAsync(cancellationToken: ct);
    }

    public async Task<bool> UpdateAppointmentDepositPaymentCommandAsync(
        Guid appointmentId,
        bool depositPayment,
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
                    await _appointments
                        .Where(predicate: entity => entity.Id == appointmentId)
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder.SetProperty(entity => entity.DepositPayment, depositPayment)
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
