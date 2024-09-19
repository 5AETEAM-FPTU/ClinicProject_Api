using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Users.GetAllDoctor;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Users.GetAllDoctor;

internal class GetAllDoctorRepository : IGetAllDoctorsRepository
{
    private readonly ClinicContext _context;
    private DbSet<Domain.Commons.Entities.Doctor> _doctors;

    public GetAllDoctorRepository(ClinicContext context)
    {
        _context = context;
        _doctors = _context.Set<Domain.Commons.Entities.Doctor>();
    }

    public Task<int> CountAllDoctorsQueryAsync(CancellationToken cancellationToken)
    {
        return _doctors.AsNoTracking().CountAsync(cancellationToken: cancellationToken);
    }

    public async Task<
        IEnumerable<Domain.Commons.Entities.Doctor>
    > FindAllDoctorsQueryAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await _doctors
            .AsNoTracking()
            .Where(predicate: doctor =>
            doctor.DoctorStaffType.TypeName.Equals("Doctor")
            //doctor.User.UserRoles.Any(userRole => userRole.Role.Name.Equals("doctor"))
            //        doctor.UserRoles.Any(userRole => userRole.Role.Name.Equals("doctor", StringComparison.OrdinalIgnoreCase))
            //        && doctor.RemovedAt == Application.Commons.Constance.CommonConstant.MIN_DATE_TIME
            //        && doctor.RemovedBy == Application.Commons.Constance.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .Select(selector: doctor => new Domain.Commons.Entities.Doctor()
            {
                Gender = doctor.Gender,
                DOB = doctor.DOB,
                Description = doctor.Description,
                Position = doctor.Position,
                Specialty = doctor.Specialty,
                Address = doctor.Address,
                Achievement = doctor.Achievement,
                Id = doctor.Id,

                User = new()
                {
                    //Id = doctor.User.Id,
                    UserName = doctor.User.UserName,
                    FullName = doctor.User.FullName,
                    PhoneNumber = doctor.User.PhoneNumber,
                    Avatar = doctor.User.Avatar,
                }

            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

    }

}
