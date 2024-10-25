using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.GetDoctorStaffProfile;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.GetDoctorStaffProfile;

internal class GetDoctorStaffProfileRepository : IGetDoctorStaffProfileRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;

    public GetDoctorStaffProfileRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
    }

    public async Task<User> GetDoctorByDoctorIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        var found = await _users
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
                Doctor = new()
                {
                    DOB = user.Doctor.DOB,
                    Description = user.Doctor.Description,
                    Position = new()
                    {
                        Name = user.Doctor.Position.Name,
                        Constant = user.Doctor.Position.Constant,
                        Id = user.Doctor.Position.Id,
                    },
                    DoctorSpecialties = user
                        .Doctor.DoctorSpecialties.Select(doctorSpecialty => new DoctorSpecialty()
                        {
                            Specialty = new()
                            {
                                Constant = doctorSpecialty.Specialty.Constant,
                                Name = doctorSpecialty.Specialty.Name,
                                Id = doctorSpecialty.Specialty.Id,
                            },
                        })
                        .ToList(),
                    Address = user.Doctor.Address,
                    Achievement = user.Doctor.Achievement,
                    IsOnDuty = user.Doctor.IsOnDuty,
                },
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return found;
    }
}
