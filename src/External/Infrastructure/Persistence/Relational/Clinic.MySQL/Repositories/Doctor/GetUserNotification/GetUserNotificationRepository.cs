using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.GetUserNotification;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Doctor.GetUserNotification;

internal class GetUserNotificationRepository : IGetUserNotificationRepository
{
    private readonly ClinicContext _context;
    private DbSet<RetreatmentNotification> _notifications;

    public GetUserNotificationRepository(ClinicContext context)
    {
        _context = context;
        _notifications = _context.Set<RetreatmentNotification>();
    }

    public async Task<IEnumerable<RetreatmentNotification>> FindAllNotification(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return await _notifications
            .Where(notification =>
                notification.PatientId == userId && notification.ExaminationDate > DateTime.Now
            )
            .Select(notification => new RetreatmentNotification()
            {
                PatientId = notification.PatientId,
                ExaminationDate = notification.ExaminationDate,
                Id = notification.Id,
                RetreatmentType = new RetreatmentType()
                {
                    Id = notification.RetreatmentType.Id,
                    Constant = notification.RetreatmentType.Constant,
                    Name = notification.RetreatmentType.Name,
                },
            })
            .ToListAsync(cancellationToken);
    }
}
