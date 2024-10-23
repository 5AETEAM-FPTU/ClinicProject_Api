using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Doctors.AddDoctor;
using Clinic.MySQL.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Doctor.AddDoctor;

internal class AddDoctorRepository : IAddDoctorRepository
{
    private readonly ClinicContext _context;
    private UserManager<User> _userManager;
    private DbSet<Gender> _gender;
    private DbSet<Position> _positions;
    private DbSet<Specialty> _specialties;

    public AddDoctorRepository(ClinicContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
        _gender = _context.Set<Gender>();
        _positions = _context.Set<Position>();
        _specialties = _context.Set<Specialty>();
    }

    public async Task<bool> CreateDoctorCommandAsync(
        User doctor,
        string userPassword,
        string roleName,
        CancellationToken cancellationToken
    )
    {
        var dbTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    var result = await _userManager.CreateAsync(
                        user: doctor,
                        password: userPassword
                    );

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateConcurrencyException();
                    }

                    result = await _userManager.AddToRoleAsync(user: doctor, role: roleName);

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateConcurrencyException();
                    }
                    var emailConfirmationToken =
                        await _userManager.GenerateEmailConfirmationTokenAsync(user: doctor);

                    await _userManager.ConfirmEmailAsync(
                        user: doctor,
                        token: emailConfirmationToken
                    );
                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);

                    await transaction.CommitAsync(cancellationToken: cancellationToken);
                    dbTransactionResult = true;
                }
                catch (Exception e)
                {
                    Console.Write(e);

                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });
        return dbTransactionResult;
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
        IEnumerable<Guid> specialtyIds,
        CancellationToken cancellationToken
    )
    {
        return _specialties.AnyAsync(
            predicate: entity => specialtyIds.Contains(entity.Id),
            cancellationToken: cancellationToken
        );
    }
}
