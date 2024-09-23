using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Token.AccessToken;
using Clinic.Application.Commons.Token.RefreshToken;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Auths.Login;

/// <summary>
///     Login request handler.
/// </summary>
public sealed class LoginHandler : IFeatureHandler<LoginRequest, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IRefreshTokenHandler _refreshTokenHandler;
    private readonly IAccessTokenHandler _accessTokenHandler;

    public LoginHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IRefreshTokenHandler refreshTokenHandler,
        IAccessTokenHandler accessTokenHandler
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _signInManager = signInManager;
        _refreshTokenHandler = refreshTokenHandler;
        _accessTokenHandler = accessTokenHandler;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="command">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    /// </returns>
    public async Task<LoginResponse> ExecuteAsync(LoginRequest command, CancellationToken ct)
    {
        // Find user by username.
        var foundUser = await _userManager.FindByNameAsync(userName: command.Username);

        // User with username does not exist.
        if (Equals(objA: foundUser, objB: default))
        {
            return new() { StatusCode = LoginResponseStatusCode.USER_IS_NOT_FOUND };
        }

        // Does user have the current password.
        var doesUserHaveCurrentPassword = await _userManager.CheckPasswordAsync(
            user: foundUser,
            password: command.Password
        );

        // Password does not belong to user.
        if (!doesUserHaveCurrentPassword)
        {
            // Is user locked out.
            var userLockedOutResult = await _signInManager.CheckPasswordSignInAsync(
                user: foundUser,
                password: command.Password,
                lockoutOnFailure: true
            );

            // User is temporary locked out.
            if (userLockedOutResult.IsLockedOut)
            {
                return new() { StatusCode = LoginResponseStatusCode.USER_IS_LOCKED_OUT };
            }

            return new() { StatusCode = LoginResponseStatusCode.USER_PASSWORD_IS_NOT_CORRECT };
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved =
            await _unitOfWork.LoginRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: foundUser.Id,
                cancellationToken: ct
            );

        // User is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            return new() { StatusCode = LoginResponseStatusCode.USER_IS_TEMPORARILY_REMOVED };
        }

        // Get found user roles.
        var foundUserRoles = await _userManager.GetRolesAsync(user: foundUser);

        // Init list of user claims.
        List<Claim> userClaims =
        [
            new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new(type: JwtRegisteredClaimNames.Sub, value: foundUser.Id.ToString()),
            new(type: "role", value: foundUserRoles[default])
        ];

        // Create new refresh token.
        RefreshToken newRefreshToken =
            new()
            {
                AccessTokenId = Guid.Parse(
                    input: userClaims
                        .First(predicate: claim =>
                            claim.Type.Equals(value: JwtRegisteredClaimNames.Jti)
                        )
                        .Value
                ),
                UserId = foundUser.Id,
                ExpiredDate = command.IsRemember
                    ? DateTime.UtcNow.AddDays(value: 7)
                    : DateTime.UtcNow.AddDays(value: 3),
                CreatedAt = DateTime.UtcNow,
                RefreshTokenValue = _refreshTokenHandler.Generate(length: 15)
            };

        // Add new refresh token to the database.
        var dbResult = await _unitOfWork.LoginRepository.CreateRefreshTokenCommandAsync(
            refreshToken: newRefreshToken,
            cancellationToken: ct
        );

        // Cannot add new refresh token to the database.
        if (!dbResult)
        {
            return new() { StatusCode = LoginResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Generate access token.
        var newAccessToken = _accessTokenHandler.GenerateSigningToken(claims: userClaims);

        var user = await _unitOfWork.LoginRepository.GetUserByUserIdQueryAsync(
            userId: foundUser.Id,
            cancellationToken: ct
        );

        return new()
        {
            StatusCode = LoginResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.RefreshTokenValue,
                User = new()
                {
                    Email = foundUser.Email,
                    AvatarUrl = user.Avatar,
                    FullName = user.FullName,
                }
            }
        };
    }
}
