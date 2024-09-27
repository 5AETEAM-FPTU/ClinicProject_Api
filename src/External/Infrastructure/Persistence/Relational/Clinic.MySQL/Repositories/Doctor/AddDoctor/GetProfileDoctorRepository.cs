using System;
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

    public AddDoctorRepository(ClinicContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
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

    public Task<bool> IsDoctorStaffTypeFoundByIdQueryAsync(
        Guid doctorStaffId,
        CancellationToken cancellationToken
    )
    {
        return Task.FromResult( false );
    }
}
