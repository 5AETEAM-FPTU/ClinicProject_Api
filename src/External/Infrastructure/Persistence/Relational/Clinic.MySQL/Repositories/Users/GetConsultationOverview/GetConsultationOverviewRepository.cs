using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Clinic.Domain.Features.Repositories.Users.GetConsultationOverview;
using Clinic.Domain.Commons.Entities;
using System.Linq;

namespace Clinic.MySQL.Repositories.Users.GetConsultationOverview;

internal class GetConsultationOverviewRepository : IGetConsultationOverviewRepository
{
    private readonly ClinicContext _context;
    private DbSet<QueueRoom> _pendings;
    private DbSet<ChatRoom> _dones;

    public GetConsultationOverviewRepository(ClinicContext context)
    {
        _context = context;
        _pendings = _context.Set<QueueRoom>();
        _dones = _context.Set<ChatRoom>();
    }

    public async Task<IEnumerable<QueueRoom>> FindAllPendingConsultationByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return await _pendings
            .Where(queue => queue.PatientId == userId)
            .Select(queue => new QueueRoom()
            {
                Id = queue.Id,
                Title = queue.Title,
                Message = queue.Message,
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<ChatRoom>> FindAllDoneConsultationByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return await 
            _dones
            .Where(done => done.PatientId == userId)
            .Select(done => new ChatRoom()
            {
                Id= done.Id,
                LastMessage = done.LastMessage,
                Doctor = new Domain.Commons.Entities.Doctor()
                {
                    User = new User()
                    {
                        FullName = done.Doctor.User.FullName,
                    }
                }
                
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }

  
}
