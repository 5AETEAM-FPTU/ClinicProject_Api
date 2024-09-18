﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Application.Commons.Token.AccessToken;
using Clinic.Application.Commons.Token.RefreshToken;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Auths.LoginWithGoogle;

/// <summary>
///     LoginWithGoogle request handler.
/// </summary>
internal sealed class LoginWithGoogleHandler
    : IFeatureHandler<LoginWithGoogleRequest, LoginWithGoogleResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IRefreshTokenHandler _refreshTokenHandler;
    private readonly IAccessTokenHandler _accessTokenHandler;
    private readonly IDefaultUserAvatarAsUrlHandler _defaultUserAvatarAsUrlHandler;

    public LoginWithGoogleHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IRefreshTokenHandler refreshTokenHandler,
        IAccessTokenHandler accessTokenHandler,
        IDefaultUserAvatarAsUrlHandler defaultUserAvatarAsUrlHandler
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _signInManager = signInManager;
        _refreshTokenHandler = refreshTokenHandler;
        _accessTokenHandler = accessTokenHandler;
        _defaultUserAvatarAsUrlHandler = defaultUserAvatarAsUrlHandler;
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
    public async Task<LoginWithGoogleResponse> ExecuteAsync(
        LoginWithGoogleRequest request,
        CancellationToken ct
    )
    {
        var googleUser = await ValidateGoogleToken(idToken: request.IdToken);

        // Find user by email
        var userFound = await _userManager.FindByEmailAsync(googleUser.Email);

        // If user not found, create new user.
        if (Equals(objA: userFound, objB: default))
        {
            // Init user
            var newUser = InitUser(user: googleUser);

            // Create user command.
            var dbResult = await _unitOfWork.LoginWithGoogleRepository.CreateUserCommandAsync(
                user: newUser,
                cancellationToken: ct
            );

            // Respond if database operation fail.
            if (!dbResult)
            {
                return new()
                {
                    StatusCode = LoginWithGoogleResponseStatusCode.DATABASE_OPERATION_FAIL
                };
            }

            userFound = newUser;
        }
        else
        {
            var isUseTemporarilyRemoved =
                await _unitOfWork.LoginWithGoogleRepository.IsUserTemporarilyRemovedByIdQueryAsync(
                    gmail: userFound.Email,
                    cancellationToken: ct
                );

            if (isUseTemporarilyRemoved)
            {
                return new()
                {
                    StatusCode = LoginWithGoogleResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
                };
            }
        }

        if (Equals(objA: googleUser, objB: default))
        {
            return new() { StatusCode = LoginWithGoogleResponseStatusCode.UNAUTHORIZE };
        }

        // Init list of user claims.
        List<Claim> userClaims =
        [
            new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new(type: JwtRegisteredClaimNames.Sub, value: userFound.Id.ToString()),
            new(type: "role", value: "user")
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
                UserId = userFound.Id,
                ExpiredDate = DateTime.UtcNow.AddDays(value: 7),
                CreatedAt = DateTime.UtcNow,
                RefreshTokenValue = _refreshTokenHandler.Generate(length: 15)
            };

        // Add new refresh token to the database.
        var dbResult2 = await _unitOfWork.LoginRepository.CreateRefreshTokenCommandAsync(
            refreshToken: newRefreshToken,
            cancellationToken: ct
        );

        // Cannot add new refresh token to the database.
        if (!dbResult2)
        {
            return new() { StatusCode = LoginWithGoogleResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Generate new access token.
        var newAccessToken = _accessTokenHandler.GenerateSigningToken(claims: userClaims);

        // Response successfully.
        return new LoginWithGoogleResponse()
        {
            StatusCode = LoginWithGoogleResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                AccessToken = newAccessToken,
                User = new()
                {
                    Email = googleUser.Email,
                    AvatarUrl = googleUser.Picture,
                    FullName = googleUser.Name
                }
            },
        };
    }

    private async Task<GoogleUser> ValidateGoogleToken(string idToken)
    {
        try
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(
                $"https://oauth2.googleapis.com/tokeninfo?id_token={idToken}"
            );

            return JsonSerializer.Deserialize<GoogleUser>(response);
        }
        catch
        {
            return default;
        }
    }

    private User InitUser(GoogleUser user)
    {
        var Id = new Guid();
        return new()
        {
            Id = Id,
            FullName = user.Name,
            UserName = user.Email,
            Email = user.Email,
            Avatar = _defaultUserAvatarAsUrlHandler.Get(),
            Patient = new() { Id = Guid.NewGuid(), UserId = Id, }
        };
    }

    public sealed record GoogleUser(string Sub, string Email, string Picture, string Name);
}