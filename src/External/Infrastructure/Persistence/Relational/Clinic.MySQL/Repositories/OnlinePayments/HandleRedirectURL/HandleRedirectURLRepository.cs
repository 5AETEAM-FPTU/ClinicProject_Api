using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.OnlinePayments.HandleRedirectURL;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.OnlinePayments.HandleRedirectURL;

/// <summary>
///     Implementation of <see cref="IHandleRedirectURLRepository"/>
/// </summary>
internal class HandleRedirectURLRepository : IHandleRedirectURLRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Appointment> _appointments;
    private readonly DbSet<OnlinePayment> _onlinePayment;
    private readonly DbSet<Clinic.Domain.Commons.Entities.Doctor> _doctors;

    public HandleRedirectURLRepository(ClinicContext context)
    {
        _context = context;
        _appointments = context.Set<Appointment>();
        _onlinePayment = context.Set<OnlinePayment>();
    }

    public async Task<bool> DeleteAppointmentCommandAsync(
        Guid appointmentId,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var payment = await _onlinePayment
                .Where(predicate: entity => entity.AppointmentId == appointmentId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            _onlinePayment.Remove(payment);
            _appointments.Remove(new Appointment { Id = appointmentId });
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            return false;
        }
    }

    public Task<Appointment> FindAppointmentByIdQueryAsync(
        Guid appointmentId,
        CancellationToken cancellationToken = default
    )
    {
        return _appointments
            .Where(predicate: entity => entity.Id == appointmentId)
            .Select(selector: entity => new Appointment()
            {
                Schedule = new()
                {
                    Doctor = new()
                    {
                        User = new() { FullName = entity.Schedule.Doctor.User.FullName }
                    }
                },
                ExaminationDate = entity.ExaminationDate,
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<OnlinePayment> FindPaymentByAppointmentIdQueryAsync(
        Guid appointmentId,
        CancellationToken cancellationToken = default
    )
    {
        return _onlinePayment
            .Where(predicate: entity => entity.AppointmentId == appointmentId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> UpdatePaymentCommandAsync(
        OnlinePayment existPaymet,
        OnlinePayment onlinePayment,
        CancellationToken cancellationToken = default
    )
    {
        var dbTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                _onlinePayment.Attach(entity: existPaymet).CurrentValues.SetValues(onlinePayment);

                try
                {
                    await _appointments
                        .Where(predicate: entity => entity.Id == existPaymet.AppointmentId)
                        .ExecuteUpdateAsync(
                            setPropertyCalls: (
                                entity => entity.SetProperty(entity => entity.DepositPayment, true)
                            )
                        );

                    dbTransactionResult =
                        await _context.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
                    await transaction.CommitAsync(cancellationToken: cancellationToken);
                }
                catch (Exception e)
                {
                    await Console.Out.WriteLineAsync(e.Message);
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return dbTransactionResult;
    }
}
