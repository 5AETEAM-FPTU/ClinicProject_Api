using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.OnlinePayments.CreateNewOnlinePayment;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.OnlinePayments.CreateNewOnlinePayment;

/// <summary>
///     Implementation of <see cref="ICreateNewOnlinePaymentRepository"/>
/// </summary>
internal class CreateNewOnlinePaymentRepository : ICreateNewOnlinePaymentRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Appointment> _appointments;
    private readonly DbSet<OnlinePayment> _onlinePayment;

    public CreateNewOnlinePaymentRepository(ClinicContext context)
    {
        _context = context;
        _appointments = context.Set<Appointment>();
        _onlinePayment = context.Set<OnlinePayment>();
    }

    public async Task<bool> CreateNewOnlinePaymentAsync(
        OnlinePayment onlinePayment,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            _onlinePayment.Add(onlinePayment);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
            return false;
        }
        return true;
    }

    public Task<bool> IsAppointmentHasDepositedAsync(
        Guid appointmentId,
        CancellationToken cancellationToken = default
    )
    {
        return _appointments.AnyAsync(
            appointment => appointment.OnlinePayment != null,
            cancellationToken
        );
    }

    public Task<bool> IsUserHasCorrectlyAppointmentWithIdAsync(
        Guid userId,
        Guid appointmentId,
        CancellationToken cancellationToken = default
    )
    {
        return _appointments.AnyAsync(
            appointment => appointment.Id == appointmentId && appointment.PatientId == userId,
            cancellationToken
        );
    }
}
