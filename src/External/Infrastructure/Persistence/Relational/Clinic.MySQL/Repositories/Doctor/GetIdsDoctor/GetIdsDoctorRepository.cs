using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.GetIdsDoctor;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Doctor.GetIdsDoctor;

/// <summary>
///     Implementation of IGetIdsDoctorRepository
/// </summary>
internal class GetIdsDoctorRepository : IGetIdsDoctorRepository
{
    private readonly ClinicContext _context;
    private DbSet<Clinic.Domain.Commons.Entities.Doctor> _doctors;

    public GetIdsDoctorRepository(ClinicContext context)
    {
        _context = context;
        _doctors = _context.Set<Clinic.Domain.Commons.Entities.Doctor>();
    }

    public async Task<IEnumerable<Guid>> FindIdsDoctorQueryAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await _doctors
            .Where(predicate: doctor =>
                doctor.User.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .Select(selector: entity => entity.UserId)
            .ToListAsync(cancellationToken);
    }
}
