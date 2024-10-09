using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Constance;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Application.Commons.Token.AccessToken;
using Clinic.Application.Commons.Token.RefreshToken;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;

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
    private readonly IConfiguration _configuration;

    public LoginWithGoogleHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IRefreshTokenHandler refreshTokenHandler,
        IAccessTokenHandler accessTokenHandler,
        IDefaultUserAvatarAsUrlHandler defaultUserAvatarAsUrlHandler,
        IConfiguration configuration
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _signInManager = signInManager;
        _refreshTokenHandler = refreshTokenHandler;
        _accessTokenHandler = accessTokenHandler;
        _defaultUserAvatarAsUrlHandler = defaultUserAvatarAsUrlHandler;
        _configuration = configuration;
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

        if (Equals(objA: googleUser, objB: default))
        {
            return new() { StatusCode = LoginWithGoogleResponseStatusCode.INVALID_GOOGLE_TOKEN, };
        }

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
                defaultPassword: _configuration["Authentication:DefaultPassword"],
                cancellationToken: ct
            );

            // Respond if database operation fail.
            if (!dbResult)
            {
                return new()
                {
                    StatusCode = LoginWithGoogleResponseStatusCode.DATABASE_OPERATION_FAIL,
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
                    StatusCode = LoginWithGoogleResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
                };
            }
        }

        // Init list of user claims.
        List<Claim> userClaims =
        [
            new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new(type: JwtRegisteredClaimNames.Sub, value: userFound.Id.ToString()),
            new(type: "role", value: "user"),
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
                RefreshTokenValue = _refreshTokenHandler.Generate(length: 15),
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
                RefreshToken = newRefreshToken.RefreshTokenValue,
                User = new()
                {
                    Email = userFound.Email,
                    AvatarUrl = userFound.Avatar,
                    FullName = userFound.FullName,
                },
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

            var googleUser = JsonConvert.DeserializeObject<GoogleUser>(response);

            return googleUser;
        }
        catch
        {
            return default;
        }
    }

    private User InitUser(GoogleUser user)
    {
        var Id = Guid.NewGuid();
        return new()
        {
            Id = Id,
            FullName = user.Name,
            UserName = user.Email,
            Email = user.Email,
            Avatar = user.Picture,
            GenderId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            Patient = new()
            {
                UserId = Id,
                Address = "default",
                DOB = CommonConstant.MIN_DATE_TIME,
                Description = "default",
            },
            CreatedAt = DateTime.UtcNow,
            CreatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
        };
    }

    public sealed record GoogleUser(string Sub, string Email, string Picture, string Name);
}
