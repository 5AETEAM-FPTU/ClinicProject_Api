using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.GetAllDoctorForStaff;
using Clinic.MySQL.Data.Context;
using Clinic.MySQL.Data.DataSeeding;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Doctor.GetAllDoctorForStaff;

/// <summary>
///    Implement of IGetAllDoctorForStaff repository.
/// </summary>
internal class GetAllDoctorForStaffRepository : IGetAllDoctorForStaffRepository
{
    private readonly ClinicContext _context;
    private DbSet<Domain.Commons.Entities.Doctor> _userDetails;

    public GetAllDoctorForStaffRepository(ClinicContext context)
    {
        _context = context;
        _userDetails = _context.Set<Domain.Commons.Entities.Doctor>();
    }

    public async Task<int> CountAllDoctorsQueryAsync(
        string keyWord,
        CancellationToken cancellationToken
    )
    {
        return await _userDetails
            .AsNoTracking()
            .Where(entity =>
                entity.User.FullName.Contains(keyWord)
                && entity.PositionId != EnumConstant.Position.HEALTHCARESTAFF_ID
            )
            .CountAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Domain.Commons.Entities.Doctor>> FindAllDoctorsQueryAsync(
        int pageIndex,
        int pageSize,
        string keyWord,
        CancellationToken cancellationToken
    )
    {
        return await _userDetails
            .Where(entity =>
                entity.User.FullName.Contains(keyWord)
                && entity.PositionId != EnumConstant.Position.HEALTHCARESTAFF_ID
            )
            .Select(selector: doctor => new Domain.Commons.Entities.Doctor()
            {
                UserId = doctor.UserId,
                DoctorSpecialties = doctor
                    .DoctorSpecialties.Select(doctorSpecialty => new DoctorSpecialty()
                    {
                        Specialty = new()
                        {
                            Constant = doctorSpecialty.Specialty.Constant,
                            Name = doctorSpecialty.Specialty.Name,
                            Id = doctorSpecialty.Specialty.Id,
                        },
                    })
                    .ToList(),
                User = new Clinic.Domain.Commons.Entities.User()
                {
                    FullName = doctor.User.FullName,
                    Avatar = doctor.User.Avatar,
                    Gender = new()
                    {
                        Name = doctor.User.Gender.Name,
                        Constant = doctor.User.Gender.Constant,
                        Id = doctor.User.Gender.Id,
                    },
                },
                IsOnDuty = doctor.IsOnDuty,
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
