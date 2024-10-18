using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.CallToken;
using Clinic.Application.Commons.Token.AccessToken;
using Clinic.Application.Commons.Token.RefreshToken;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Auths.RefreshAccessToken;

/// <summary>
///     RefreshAccessToken request handler.
/// </summary>
internal sealed class RefreshAccessTokenHandler
    : IFeatureHandler<RefreshAccessTokenRequest, RefreshAccessTokenResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRefreshTokenHandler _refreshTokenHandler;
    private readonly IAccessTokenHandler _accessTokenHandler;
    private readonly ICallTokenHandler _callTokenHandler;

    public RefreshAccessTokenHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        IRefreshTokenHandler refreshTokenHandler,
        IAccessTokenHandler accessTokenHandler,
        ICallTokenHandler callTokenHandler
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _refreshTokenHandler = refreshTokenHandler;
        _accessTokenHandler = accessTokenHandler;
        _callTokenHandler = callTokenHandler;
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
    public async Task<RefreshAccessTokenResponse> ExecuteAsync(
        RefreshAccessTokenRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find refresh token by its value.
        var foundRefreshToken =
            await _unitOfWork.RefreshAccessTokenRepository.FindByRefreshTokenValueQueryAsync(
                refreshTokenValue: request.RefreshToken,
                cancellationToken: cancellationToken
            );

        // Responds if refresh token is not found.
        if (Equals(objA: foundRefreshToken, objB: default))
        {
            return new RefreshAccessTokenResponse()
            {
                StatusCode = RefreshAccessTokenResponseStatusCode.REFRESH_TOKEN_IS_NOT_FOUND
            };
        }

        // Responds if refresh token is expired.
        if (DateTime.UtcNow > foundRefreshToken.ExpiredDate)
        {
            return new()
            {
                StatusCode = RefreshAccessTokenResponseStatusCode.REFRESH_TOKEN_IS_EXPIRED
            };
        }

        // Init new list of user claims.
        List<Claim> userClaims =
        [
            new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new(
                type: JwtRegisteredClaimNames.Sub,
                value: _httpContextAccessor.HttpContext.User.FindFirstValue(
                    claimType: JwtRegisteredClaimNames.Sub
                )
            ),
            new(
                type: "role",
                value: _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role")
            )
        ];

        // Generate new access token.
        var newAccessTokenId = _accessTokenHandler.GenerateSigningToken(claims: userClaims);
        var newCallAccessToken = _callTokenHandler.GenerateAccessToken(userId: userClaims[1].Value);

        // Generate new refresh token value.
        var newRefreshTokenValue = _refreshTokenHandler.Generate(length: 15);

        // Update current refresh token.
        var dbResult =
            await _unitOfWork.RefreshAccessTokenRepository.UpdateRefreshTokenCommandAsync(
                oldRefreshTokenValue: request.RefreshToken,
                newRefreshTokenValue: newRefreshTokenValue,
                newAccessTokenId: Guid.Parse(input: userClaims[0].Value),
                cancellationToken: cancellationToken
            );

        // Response if can't add new refresh token
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RefreshAccessTokenResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RefreshAccessTokenResponse()
        {
            StatusCode = RefreshAccessTokenResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                AccessToken = newAccessTokenId,
                RefreshToken = newRefreshTokenValue,
                CallAccessToken = newCallAccessToken
            },
        };
    }
}
