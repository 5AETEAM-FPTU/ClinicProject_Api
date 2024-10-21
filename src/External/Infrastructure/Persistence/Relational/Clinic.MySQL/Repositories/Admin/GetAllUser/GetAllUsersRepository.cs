﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.GetAllUser;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Admin.GetAllUser;

internal class GetAllUsersRepository : IGetAllUsersRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _patient;

    public GetAllUsersRepository(ClinicContext context)
    {
        _context = context;
        _patient = _context.Set<User>();
    }

    public async Task<int> CountAllUserQueryAsync(CancellationToken cancellationToken)
    {
        return await _patient
            .Where(predicate: user =>
                user.Patient != null
                && user.RemovedAt == Application.Commons.Constance.CommonConstant.MIN_DATE_TIME
                && user.RemovedBy
                    == Application.Commons.Constance.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .CountAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<User>> FindUserByIdQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        return await _patient
            .AsNoTracking()
            .Where(predicate: user =>
                user.Patient != null
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
                Patient = new()
                {
                    DOB = user.Patient.DOB,
                    Description = user.Patient.Description,
                    Address = user.Patient.Address,
                },
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
