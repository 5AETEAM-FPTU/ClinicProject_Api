using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Feedbacks.DoctorGetAllFeedbacks;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Feedbacks.DoctorGetAllFeedbacks;

public class DoctorGetAllFeedbacksRepository: IDoctorGetAllFeedbacksRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Feedback> _feedbacks;

    public DoctorGetAllFeedbacksRepository(ClinicContext context)
    {
        _context = context;
        _feedbacks = _context.Set<Feedback>();  
    }

    public async Task<double> GetRatingQueryAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _feedbacks
            .Where(entity => entity.Appointment.Schedule.DoctorId == userId)
            .AverageAsync(entity => entity.Vote);
    }

    public async Task<int> CountAllFeedbacksQueryAsync(Guid userId, int? vote, CancellationToken cancellationToken)
    {
        var query = _feedbacks
            .AsNoTracking()
            .Where(entity => entity.Appointment.Schedule.DoctorId == userId)
            .AsQueryable();

        if (vote != null)
        {
            query = query.Where(entity => entity.Vote == vote);
        }

        return await query.CountAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Feedback>> GetAllFeedbacksQueryAsync(int pageIndex, int pageSize, Guid userId, int? vote, CancellationToken cancellationToken)
    {
        var result = _feedbacks
            .AsNoTracking()
            .Where(entity => entity.Appointment.Schedule.DoctorId == userId)
            .AsQueryable();

        if (vote != null)
        {
            result = result.Where(entity => entity.Vote == vote);
        }

        return await result.Select(entity => new Feedback()
            {
                Id = entity.Id,
                Comment = entity.Comment,
                Vote = entity.Vote,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
