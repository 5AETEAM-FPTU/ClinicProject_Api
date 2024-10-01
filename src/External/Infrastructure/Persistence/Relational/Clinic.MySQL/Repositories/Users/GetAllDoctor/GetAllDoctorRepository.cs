using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Users.GetAllDoctor;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Users.GetAllDoctor;

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
        return _users.AsNoTracking().CountAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<User>> FindAllDoctorsQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        return await _users
            .AsNoTracking()       
            .Where(predicate: user => user.Doctor!= null)
            .Where(predicate: doctor =>
                    doctor != null 
                && doctor.RemovedAt == Application.Commons.Constance.CommonConstant.MIN_DATE_TIME
                && doctor.RemovedBy == Application.Commons.Constance.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
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
                    Constant = user.Gender.Constant 
                },
                Doctor = new()
                {
                    DOB = user.Doctor.DOB,
                    Description = user.Doctor.Description,
                    Position = user.Doctor.Position,
                    DoctorSpecialties = user
                        .Doctor.DoctorSpecialties.Select(doctorSpecialty => new DoctorSpecialty()
                        {
                            Specialty = new Specialty 
                            { 
                                Id = doctorSpecialty.Specialty.Id,
                                Name = doctorSpecialty.Specialty.Name 
                            }
                        })
                        .ToList(),
                    Address = user.Doctor.Address,
                    Achievement = user.Doctor.Achievement
                }
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
