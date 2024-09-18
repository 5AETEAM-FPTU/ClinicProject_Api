using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Users.GetProfileUser;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

    public Task<User> GetUserByUserIdQueryAsync(
        Guid userId, 
        CancellationToken cancellationToken
    )
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

                Patient = new()
                {
                    Gender = user.Patient.Gender,
                    DOB = user.Patient.DOB,
                    Address = user.Patient.Address,
                    Description = user.Patient.Description
                }

            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

    }

}
