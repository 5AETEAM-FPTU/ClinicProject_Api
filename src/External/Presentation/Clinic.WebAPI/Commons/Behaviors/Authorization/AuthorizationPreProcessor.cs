using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Commons.Others;
using Clinic.WebAPI.Commons.AppCodes;
using Clinic.WebAPI.Commons.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Clinic.WebAPI.Commons.Behaviors.Authorization;

/// <summary>
///     Pre-processor for <see cref="Request"/>
/// </summary>
internal sealed class AuthorizationPreProcessor<TRequest>
    : PreProcessor<TRequest, AuthorizationStateBag>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public AuthorizationPreProcessor(
        IServiceScopeFactory serviceScopeFactory,
        TokenValidationParameters tokenValidationParameters
    )
    {
        _serviceScopeFactory = serviceScopeFactory;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public override async Task PreProcessAsync(
        IPreProcessorContext<TRequest> context,
        AuthorizationStateBag state,
        CancellationToken ct
    )
    {
        JsonWebTokenHandler jsonWebTokenHandler = new();

        // Validate access token.
        var validateTokenResult = await jsonWebTokenHandler.ValidateTokenAsync(
            token: context.HttpContext.Request.Headers.Authorization[0].Split(separator: " ")[1],
            validationParameters: _tokenValidationParameters
        );

        // Token is not valid.
        if (
            !validateTokenResult.IsValid
            || validateTokenResult.SecurityToken.ValidTo < DateTime.UtcNow
        )
        {
            await SendResponseAsync(statusCode: CommonAppCode.FORBIDDEN, context: context, ct: ct);
        }

        // Get the jti claim from the access token.
        var jtiClaim = context.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Jti
        );

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var repository = scope.Resolve<IAuthorizationRepository>();

        // Does refresh token exist by access token id.
        var isRefreshTokenFound = await repository.IsRefreshTokenFoundByAccessTokenIdQueryAsync(
            accessTokenId: Guid.Parse(input: jtiClaim),
            cancellationToken: ct
        );

        // Refresh token is not found by access token id.
        if (!isRefreshTokenFound)
        {
            await SendResponseAsync(statusCode: CommonAppCode.FORBIDDEN, context: context, ct: ct);
        }

        var userManager = scope.Resolve<UserManager<User>>();

        // Get the sub claim from the access token.
        var subClaim = context.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Find user by user id.
        var foundUser = await userManager.FindByIdAsync(
            userId: Guid.Parse(input: subClaim).ToString()
        );

        // User is not found
        if (Equals(objA: foundUser, objB: default))
        {
            await SendResponseAsync(statusCode: CommonAppCode.FORBIDDEN, context: context, ct: ct);
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved = await repository.IsUserTemporarilyRemovedQueryAsync(
            userId: foundUser.Id,
            cancellationToken: ct
        );

        // User is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            await SendResponseAsync(statusCode: CommonAppCode.FORBIDDEN, context: context, ct: ct);
        }

        // Get the role claim from the access token.
        var roleClaim = context.HttpContext.User.FindFirstValue(claimType: "role");

        // Is user in role.
        var isUserInRole = await userManager.IsInRoleAsync(user: foundUser, role: roleClaim);

        // User is not in role.
        if (!isUserInRole)
        {
            await SendResponseAsync(statusCode: CommonAppCode.FORBIDDEN, context: context, ct: ct);
        }
    }

    private static Task SendResponseAsync(
        CommonAppCode statusCode,
        IPreProcessorContext<TRequest> context,
        CancellationToken ct
    )
    {
        var httpResponse = new CommonApiResponse()
        {
            AppCode = statusCode.ToString(),
            ErrorMessages = ["You don't have permission to access this resource"]
        };

        context.HttpContext.MarkResponseStart();

        return context.HttpContext.Response.SendAsync(
            response: httpResponse,
            statusCode: StatusCodes.Status403Forbidden,
            cancellation: ct
        );
    }
}
