using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.GetUserInforById;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Doctor.GetUserInforById;

internal class GetUserInforByIdRepository : IGetUserInforByIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;

    public GetUserInforByIdRepository(ClinicContext context)
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
                Id = user.Id,
                FullName = user.FullName,
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
}
