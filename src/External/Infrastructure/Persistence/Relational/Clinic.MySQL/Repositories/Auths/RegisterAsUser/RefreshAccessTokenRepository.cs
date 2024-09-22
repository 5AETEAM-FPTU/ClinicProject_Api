using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.RegisterAsUser;
using Clinic.MySQL.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Auths.RegisterAsUser;

/// <summary>
///     Implement for RegisterAsUser Repository
/// </summary>
public class RegisterAsUserRepository : IRegisterAsUserRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;
    private DbSet<RefreshToken> _refreshTokens;

    public RegisterAsUserRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
        _refreshTokens = _context.Set<RefreshToken>();
    }

    public async Task<bool> CreateUserAndAddUserRoleCommandAsync(
        User newUser,
        string userPassword,
        UserManager<User> userManager,
        string userRole,
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
                    var result = await userManager.CreateAsync(
                        user: newUser,
                        password: userPassword
                    );

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateConcurrencyException();
                    }

                    result = await userManager.AddToRoleAsync(user: newUser, role: userRole);

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateConcurrencyException();
                    }

                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);

                    await transaction.CommitAsync(cancellationToken: cancellationToken);

                    dbTransactionResult = true;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });
        return dbTransactionResult;
    }

    public Task<bool> IsUserFoundByNormalizedEmailQueryAsync(
        string email,
        CancellationToken cancellation
    )
    {
        email = email.ToUpper();

        return _users.AnyAsync(
            predicate: user => user.NormalizedEmail.Equals(email),
            cancellationToken: cancellation
        );
    }

    public Task<bool> IsUserFoundByNormalizedUsernameQueryAsync(
        string username,
        CancellationToken cancellation
    )
    {
        username = username.ToUpper();

        return _users.AnyAsync(
            predicate: user => user.NormalizedUserName.Equals(username),
            cancellationToken: cancellation
        );
    }
}
