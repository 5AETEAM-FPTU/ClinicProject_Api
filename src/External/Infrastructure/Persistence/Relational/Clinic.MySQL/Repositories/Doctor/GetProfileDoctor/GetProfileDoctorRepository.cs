using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.GetProfileDoctor;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Doctor.GetProfileDoctor;

internal class GetProfileDoctorRepository : IGetProfileDoctorRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;

    public GetProfileDoctorRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
    }


    public Task<User> GetDoctorByDoctorIdQueryAsync(
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

                Doctor = new()
                {
                    Gender = user.Doctor.Gender,
                    DOB = user.Doctor.DOB,
                    Description = user.Doctor.Description,
                    Position = user.Doctor.Position,
                    Specialty = user.Doctor.Specialty,
                    Address = user.Doctor.Address,
                    Achievement = user.Doctor.Achievement
                }
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
                entity.Id == userId && entity.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            cancellationToken: cancellationToken
        );
    }
}
