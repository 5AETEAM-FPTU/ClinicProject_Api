using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Users.SendFeedBack;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Users.SendFeedBack;

internal class SendFeedBackRepository : ISendFeedBackRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Feedback> _feedbacks;
    private readonly DbSet<Appointment> _appointments;

    public SendFeedBackRepository(ClinicContext context)
    {
        _context = context;
        _feedbacks = _context.Set<Feedback>();
        _appointments = _context.Set<Appointment>();
    }

    public async Task<bool> CreateNewFeedBack(
        Feedback feedback,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            _feedbacks.Add(feedback);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
            return false;
        }
        return true;
    }

    public async Task<bool> IsAppointmentDone(
        Guid appointmentId,
        CancellationToken cancellationToken = default
    )
    {
        return await _appointments.AnyAsync(
            appointment => appointment.AppointmentStatus.Constant.Equals("Completed"),
            cancellationToken
        );
    }

    public async Task<bool> IsExistFeedBack(
        Guid appointmentId,
        CancellationToken cancellationToken = default
    )
    {
        return await _feedbacks.AnyAsync(
            feedback => feedback.AppointmentId == appointmentId,
            cancellationToken
        );
    }
}
