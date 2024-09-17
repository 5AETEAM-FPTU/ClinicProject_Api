using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Mail;
using Clinic.Application.Commons.Token.OTP;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Application.Features.Auths.ChangingPassword;

/// <summary>
///     ChangingPassword request handler.
/// </summary>
internal sealed class ChangingPasswordHandler
    : IFeatureHandler<ChangingPasswordRequest, ChangingPasswordResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly ISendingMailHandler _sendingMailHandler;
    private readonly IOTPHandler _otPHandler;

    public ChangingPasswordHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        ISendingMailHandler sendingMailHandler,
        IOTPHandler otPHandler
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _sendingMailHandler = sendingMailHandler;
        _otPHandler = otPHandler;
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
    public async Task<ChangingPasswordResponse> ExecuteAsync(
        ChangingPasswordRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get the user token by reset password token.
        var foundUserToken =
            await _unitOfWork.ChangingPasswordRepository.FindUserTokenByResetPasswordTokenQueryAsync(
                passwordResetToken: request.ResetPasswordToken,
                cancellationToken: cancellationToken
            );

        // Respond if reset password token is not found by its value.
        if (Equals(objA: foundUserToken.Value, objB: default))
        {
            return new()
            {
                StatusCode = ChangingPasswordResponseStatusCode.RESET_PASSWORD_TOKEN_IS_NOT_FOUND
            };
        }

        // Checking the otp code expired or not?
        if (foundUserToken.ExpiredAt < DateTime.UtcNow)
        {
            return new() { StatusCode = ChangingPasswordResponseStatusCode.OTP_CODE_IS_EXPIRED };
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved =
            await _unitOfWork.ChangingPasswordRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: foundUserToken.UserId,
                cancellationToken: cancellationToken
            );

        // Respond if user is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = ChangingPasswordResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        // Find the user by user id.
        var foundUser = await _userManager.FindByIdAsync(userId: foundUserToken.UserId.ToString());

        // Is new user password valid.
        var isPasswordValid = await ValidateUserPasswordAsync(
            newUser: foundUser,
            newPassword: request.NewPassword
        );

        // Respond if is not valid.
        if (!isPasswordValid)
        {
            return new()
            {
                StatusCode = ChangingPasswordResponseStatusCode.NEW_PASSWORD_IS_NOT_VALID
            };
        }

        // Responds if cannot reset user password.
        var removePasswordResult = await _userManager.RemovePasswordAsync(user: foundUser);
        if (!removePasswordResult.Succeeded)
        {
            return new ChangingPasswordResponse()
            {
                StatusCode = ChangingPasswordResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Responds if cannot reset user password.
        var addPasswordResult = await _userManager.AddPasswordAsync(
            user: foundUser,
            password: request.NewPassword
        );
        if (!addPasswordResult.Succeeded)
        {
            return new ChangingPasswordResponse()
            {
                StatusCode = ChangingPasswordResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Remove the rset password token.
        var dbResult =
            await _unitOfWork.ChangingPasswordRepository.RemoveUserResetPasswordTokenCommandAsync(
                resetPasswordToken: request.ResetPasswordToken,
                cancellationToken: cancellationToken
            );

        // Cannot remove the reset password token.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = ChangingPasswordResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new ChangingPasswordResponse()
        {
            StatusCode = ChangingPasswordResponseStatusCode.OPERATION_SUCCESS,
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
