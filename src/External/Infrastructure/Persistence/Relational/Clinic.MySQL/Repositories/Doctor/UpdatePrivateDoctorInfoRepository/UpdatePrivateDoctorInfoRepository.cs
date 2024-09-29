using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.UpdatePrivateDoctorInfo;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Doctor.UpdatePrivateDoctorInfoRepository;

internal class UpdatePrivateDoctorInfoRepository : IUpdatePrivateDoctorInfoRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;
    private DbSet<Gender> _gender;
    private DbSet<Position> _positions;
    private DbSet<Specialty> _specialties;
    private DbSet<DoctorSpecialty> _doctorSpecialty;

    public UpdatePrivateDoctorInfoRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
        _gender = _context.Set<Gender>();
        _positions = _context.Set<Position>();
        _specialties = _context.Set<Specialty>();
        _doctorSpecialty = _context.Set<DoctorSpecialty>();
    }

    public async Task<User> GetDoctorByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _users
            .Include(u => u.Doctor)
            .ThenInclude(doctor => doctor.DoctorSpecialties)
            .ThenInclude(specicalty => specicalty.Specialty)
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<bool> UpdatePrivateDoctorInfoByIdCommandAsync(
        User user,
        IEnumerable<Guid> reqSpecialties,
        CancellationToken cancellationToken
    )
    {
        if (!Equals(user.Doctor, null))
        {
            _doctorSpecialty.RemoveRange(user.Doctor.DoctorSpecialties);

            var newSpecialties = reqSpecialties
                .Select(id => new DoctorSpecialty { DoctorId = user.Id, SpecialtyID = id })
                .ToList();

            await _doctorSpecialty.AddRangeAsync(newSpecialties, cancellationToken);
        }
        _context.Users.Update(user);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public Task<bool> IsGenderFoundByIdQueryAsync(
        Guid genderId,
        CancellationToken cancellationToken
    )
    {
        return _gender.AnyAsync(
            predicate: entity => entity.Id == genderId,
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsPositionFoundByIdQueryAsync(
        Guid positionId,
        CancellationToken cancellationToken
    )
    {
        return _positions.AnyAsync(
            predicate: entity => entity.Id == positionId,
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsSpecialtyFoundByIdQueryAsync(
        Guid specialtyId,
        CancellationToken cancellationToken
    )
    {
        return _specialties.AnyAsync(
            predicate: entity => entity.Id == specialtyId,
            cancellationToken: cancellationToken
        );
    }
}
