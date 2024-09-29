using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Auths.LoginWithGoogle;
using Clinic.MySQL.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Auths.LoginWithGoogle;

/// <summary>
///     Implement for LoginWithGoogle Repository
/// </summary>
public class LoginWithGoogleRepository : ILoginWithGoogleRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;
    private DbSet<RefreshToken> _refreshTokens;
    private readonly UserManager<User> _userManager;

    public LoginWithGoogleRepository(ClinicContext context, UserManager<User> userManager)
    {
        _context = context;
        _users = _context.Set<User>();
        _refreshTokens = _context.Set<RefreshToken>();
        _userManager = userManager;
    }

    public async Task<bool> CreateRefreshTokenCommandAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _refreshTokens
                .Where(reToken => reToken.UserId.Equals(refreshToken.UserId))
                .ExecuteDeleteAsync(cancellationToken: cancellationToken);

            await _refreshTokens.AddAsync(
                entity: refreshToken,
                cancellationToken: cancellationToken
            );
            await _context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public async Task<bool> CreateUserCommandAsync(
        User user,
        string defaultPassword,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _userManager.CreateAsync(user: user, password: defaultPassword);
            await _userManager.AddToRoleAsync(user: user, role: "user");
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(
                user: user
            );
            await _userManager.ConfirmEmailAsync(user: user, token: emailConfirmationToken);
            await _context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public Task<bool> IsUserFoundByEmailQueryAsync(
        string gmail,
        CancellationToken cancellationToken
    )
    {
        return _context.Users.AnyAsync(
            entity => entity.NormalizedEmail == gmail.ToUpper(),
            cancellationToken
        );
    }

    public Task<bool> IsUserTemporarilyRemovedByIdQueryAsync(
        string gmail,
        CancellationToken cancellationToken
    )
    {
        return _context.Users.AnyAsync(
            entity => entity.Email == gmail && entity.RemovedAt != CommonConstant.MIN_DATE_TIME,
            cancellationToken
        );
    }
}
