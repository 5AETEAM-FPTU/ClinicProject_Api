using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.GetAllDoctor;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.GetAllDoctor;

internal class GetAllDoctorRepository : IGetAllDoctorsRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;

    public GetAllDoctorRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
    }

    public Task<int> CountAllDoctorsQueryAsync(CancellationToken cancellationToken)
    {
        return _users
            .AsNoTracking()
            .Where(predicate: user =>
                user.Doctor != null
                && user.RemovedAt == Application.Commons.Constance.CommonConstant.MIN_DATE_TIME
                && user.RemovedBy
                    == Application.Commons.Constance.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .CountAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<User>> FindAllDoctorsQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        return await _users
            .AsNoTracking()
            .Where(predicate: user =>
                user.Doctor != null
                && user.RemovedAt == Application.Commons.Constance.CommonConstant.MIN_DATE_TIME
                && user.RemovedBy
                    == Application.Commons.Constance.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .Select(selector: user => new User()
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar,
                Gender = new()
                {
                    Id = user.Gender.Id,
                    Name = user.Gender.Name,
                    Constant = user.Gender.Constant,
                },
                Doctor = new()
                {
                    DOB = user.Doctor.DOB,
                    Description = user.Doctor.Description,
                    Position = new()
                    {
                        Id = user.Doctor.Position.Id,
                        Name = user.Doctor.Position.Name,
                        Constant = user.Doctor.Position.Constant,
                    },
                    DoctorSpecialties = user
                        .Doctor.DoctorSpecialties.Select(doctorSpecialty => new DoctorSpecialty()
                        {
                            Specialty = new Specialty
                            {
                                Id = doctorSpecialty.Specialty.Id,
                                Name = doctorSpecialty.Specialty.Name,
                                Constant = doctorSpecialty.Specialty.Constant,
                            },
                        })
                        .ToList(),
                    Address = user.Doctor.Address,
                    Achievement = user.Doctor.Achievement,
                    IsOnDuty = user.Doctor.IsOnDuty,
                },
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
