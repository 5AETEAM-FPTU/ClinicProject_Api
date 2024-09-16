using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Mail;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Application.Features.Auths.ForgotPassword;

/// <summary>
///     ForgotPassword request handler.
/// </summary>
internal sealed class ForgotPasswordHandler
    : IFeatureHandler<ForgotPasswordRequest, ForgotPasswordResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly ISendingMailHandler _sendingMailHandler;

    public ForgotPasswordHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        ISendingMailHandler sendingMailHandler
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _sendingMailHandler = sendingMailHandler;
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
    public async Task<ForgotPasswordResponse> ExecuteAsync(
        ForgotPasswordRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find user by email.
        var foundUser = await _userManager.FindByEmailAsync(email: request.Email);

        // Responds if email is not found.
        if (Equals(objA: foundUser, objB: default))
        {
            return new()
            {
                StatusCode = ForgotPasswordResponseStatusCode.USER_WITH_EMAIL_IS_NOT_FOUND
            };
        }

        // Is user not temporarily removed.
        var isUserNotTemporarilyRemoved =
            await _unitOfWork.ForgotPasswordRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: foundUser.Id,
                cancellationToken: cancellationToken
            );

        // Responds if user is temporarily removed.
        if (!isUserNotTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = ForgotPasswordResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            };
        }

        // Generate password reset token.
        var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(
            user: foundUser
        );

        // Add password reset token to database.
        var dbResult = await _unitOfWork.ForgotPasswordRepository.AddResetPasswordTokenCommandAsync(
            newResetPasswordToken: InitNewResetPasswordToken(
                userId: foundUser.Id,
                passwordResetToken: passwordResetToken
            ),
            cancellationToken: cancellationToken
        );

        // Responds if cannot add password reset token.
        if (!dbResult)
        {
            return new() { StatusCode = ForgotPasswordResponseStatusCode.DATABASE_OPERATION_FAIL, };
        }

        // Create mail content to sends.
        var mailContent = await _sendingMailHandler.GetUserResetPasswordMailContentAsync(
            to: request.Email,
            subject: "Changing password",
            resetPasswordToken: passwordResetToken,
            cancellationToken: cancellationToken
        );

        // Sending user reset password mail.
        var mailSendingResult = await _sendingMailHandler.SendAsync(
            mailContent: mailContent,
            cancellationToken: cancellationToken
        );

        // Responds if cannot send mail.
        if (!mailSendingResult)
        {
            return new()
            {
                StatusCode = ForgotPasswordResponseStatusCode.SENDING_USER_RESET_PASSWORD_MAIL_FAIL
            };
        }

        // Response successfully.
        return new ForgotPasswordResponse()
        {
            StatusCode = ForgotPasswordResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private static UserToken InitNewResetPasswordToken(Guid userId, string passwordResetToken)
    {
        return new()
        {
            LoginProvider = Guid.NewGuid().ToString(),
            Name = "PasswordResetToken",
            UserId = userId,
            Value = passwordResetToken,
            ExpiredAt = DateTime.UtcNow.AddMinutes(5).ToUniversalTime(),
        };
    }
}
