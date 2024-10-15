using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Notification.CreateRetreatmentNotification;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Notification.CreateRetreatmentNotification;

internal class CreateRetreatmentNotificationRepository : ICreateRetreatmentNotificationRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<RetreatmentNotification> _notifications;

    public CreateRetreatmentNotificationRepository(ClinicContext context)
    {
        _context = context;
        _notifications = _context.Set<RetreatmentNotification>();
    }

    public async Task<bool> CreateNewNotification(
        RetreatmentNotification notification,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            _notifications.Add(notification);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
            return false;
        }
        return true;
    }

    public async Task<bool> IsExistNotification(
        DateTime examinationDate,
        CancellationToken cancellationToken = default
    )
    {
        return await _notifications.AnyAsync(
            notification => notification.ExaminationDate.Date.Equals(examinationDate.Date),
            cancellationToken
        );
    }
}
