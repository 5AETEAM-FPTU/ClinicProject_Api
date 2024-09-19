﻿using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Users.GetAllDoctor;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Users.GetAllDoctor;

internal class GetAllDoctorRepository : IGetAllDoctorsRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _doctors;

    public GetAllDoctorRepository(ClinicContext context)
    {
        _context = context;
        _doctors = _context.Set<User>();
    }

    public async Task<
        IEnumerable<User>
    > FindAllDoctorsQueryAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await _doctors
            .AsNoTracking()
            .Where(predicate: doctor =>
                doctor.RemovedAt == Application.Commons.Constance.CommonConstant.MIN_DATE_TIME
                && doctor.RemovedBy == Application.Commons.Constance.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
        )
            .Select(selector: doctor => new User()
            {
                Id = doctor.Id,
                UserName = doctor.UserName,
                FullName = doctor.FullName,
                PhoneNumber = doctor.PhoneNumber,
                Avatar = doctor.Avatar,

                Doctor = new()
                {
                    Gender = doctor.Doctor.Gender,
                    DOB = doctor.Doctor.DOB,
                    Description = doctor.Doctor.Description,
                    Position = doctor.Doctor.Position,
                    Specialty = doctor.Doctor.Specialty,
                    Address = doctor.Doctor.Address,
                    Achievement = doctor.Doctor.Achievement
                }
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<int> CountAllDoctorsQueryAsync(CancellationToken cancellationToken)
    {
        return _doctors.AsNoTracking().CountAsync(cancellationToken: cancellationToken);
    }
}
