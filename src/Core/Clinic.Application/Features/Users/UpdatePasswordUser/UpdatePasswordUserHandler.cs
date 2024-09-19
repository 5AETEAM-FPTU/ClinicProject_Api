using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clinic.Application.Features.Users.UpdatePasswordUser;

/// <summary>
///     UpdatePasswordUser Handler
/// </summary>
public class UpdatePasswordUserHandler
    : IFeatureHandler<UpdatePasswordUserRequest, UpdatePasswordUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<User> _userManager;

    public UpdatePasswordUserHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor contextAccessor,
        UserManager<User> userManager
    )
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    public async Task<UpdatePasswordUserResponse> ExecuteAsync(
        UpdatePasswordUserRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = _contextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Find user by userId.
        var userFound = await _userManager.FindByIdAsync(userId);

        // Respond if user is not found.
        if (Equals(objA: userFound, objB: default))
        {
            return new() { StatusCode = UpdatePasswordUserResponseStatusCode.USER_IS_NOT_FOUND };
        }

        // Is user temporarily removed by Id.
        var isUserTemporarilyRemoved =
            await _unitOfWork.UpdatePasswordUserRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: Guid.Parse(userId),
                cancellationToken: cancellationToken
            );

        // Respond if user is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = UpdatePasswordUserResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        // Is new user password valid.
        var isPasswordValid = await ValidateUserPasswordAsync(
            newUser: userFound,
            newPassword: request.NewPassword
        );

        // Respond if is not valid.
        if (!isPasswordValid)
        {
            return new()
            {
                StatusCode = UpdatePasswordUserResponseStatusCode.NEW_PASSWORD_IS_NOT_VALID
            };
        }
        // Update password operation.
        var result = await _userManager.ChangePasswordAsync(
            userFound,
            request.CurrentPassword,
            request.NewPassword
        );

        if (!result.Succeeded)
        {
            return new()
            {
                StatusCode = UpdatePasswordUserResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new UpdatePasswordUserResponse()
        {
            StatusCode = UpdatePasswordUserResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    /// <summary>
    ///     Validates the password of a newly created user.
    /// </summary>
    /// <param name="newUser">
    ///     The newly created user.
    /// </param>
    /// <param name="newPassword">
    ///     The password to be validated.
    /// </param>
    /// <returns>
    ///     True if the password is valid, False otherwise.
    /// </returns>
    private async Task<bool> ValidateUserPasswordAsync(User newUser, string newPassword)
    {
        IdentityResult result = default;

        foreach (var validator in _userManager.PasswordValidators)
        {
            result = await validator.ValidateAsync(
                manager: _userManager,
                user: newUser,
                password: newPassword
            );
        }

        if (Equals(objA: result, objB: default))
        {
            return false;
        }

        return result.Succeeded;
    }
}
