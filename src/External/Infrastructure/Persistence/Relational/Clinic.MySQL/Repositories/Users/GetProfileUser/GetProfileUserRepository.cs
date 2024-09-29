using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Users.GetProfileUser;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Users.GetProfileUser;

internal class GetProfileUserRepository : IGetProfileUserRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;

    public GetProfileUserRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
    }

    public Task<User> GetUserByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _users
            .AsNoTracking()
            .Where(predicate: user => user.Id == userId)
            .Select(selector: user => new User()
            {
                UserName = user.UserName,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar,
                Gender = new()
                {
                    Name = user.Gender.Name,
                    Constant = user.Gender.Constant,
                    Id = user.Gender.Id,
                },
                Patient = new()
                {
                    DOB = user.Patient.DOB,
                    Address = user.Patient.Address,
                    Description = user.Patient.Description,
                },
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _users.AnyAsync(
            predicate: entity =>
                entity.Id == userId && entity.RemovedAt != CommonConstant.MIN_DATE_TIME,
            cancellationToken: cancellationToken
        );
    }
}
