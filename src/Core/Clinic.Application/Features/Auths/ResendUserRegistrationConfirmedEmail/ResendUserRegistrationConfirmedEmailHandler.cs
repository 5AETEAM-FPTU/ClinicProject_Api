using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Application.Commons.Mail;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Clinic.Application.Features.Auths.ResendUserRegistrationConfirmedEmail;

/// <summary>
///     ResendUserRegistrationConfirmedEmail request handler.
/// </summary>
internal sealed class ResendUserRegistrationConfirmedEmailHandler
    : IFeatureHandler<
        ResendUserRegistrationConfirmedEmailRequest,
        ResendUserRegistrationConfirmedEmailResponse
    >
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IDefaultUserAvatarAsUrlHandler _defaultUserAvatarAsUrlHandler;
    private readonly ISendingMailHandler _sendingMailHandler;

    public ResendUserRegistrationConfirmedEmailHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IDefaultUserAvatarAsUrlHandler defaultUserAvatarAsUrlHandler,
        ISendingMailHandler sendingMailHandler
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _defaultUserAvatarAsUrlHandler = defaultUserAvatarAsUrlHandler;
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
    public async Task<ResendUserRegistrationConfirmedEmailResponse> ExecuteAsync(
        ResendUserRegistrationConfirmedEmailRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find user by username.
        var foundUser = await _userManager.FindByEmailAsync(email: request.Email);

        // Responds if user is not found by username.
        if (Equals(objA: foundUser, objB: default))
        {
            return new()
            {
                StatusCode =
                    ResendUserRegistrationConfirmedEmailResponseStatusCode.USER_IS_NOT_FOUND
            };
        }
        // Is email confirmed.
        var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(foundUser);
        if (isEmailConfirmed)
        {
            return new()
            {
                StatusCode =
                    ResendUserRegistrationConfirmedEmailResponseStatusCode.USER_HAS_CONFIRMED_EMAIL
            };
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved =
            await _unitOfWork.ResendUserRegistrationConfirmedEmailRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: foundUser.Id,
                cancellationToken: cancellationToken
            );

        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    ResendUserRegistrationConfirmedEmailResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        // Send account creation confirmation mail to user.
        var mailSendingResult = await SendingConfirmationMailToUserAsync(
            newUser: foundUser,
            cancellationToken: cancellationToken
        );

        // Responds if cannot send mail.
        if (!mailSendingResult)
        {
            return new()
            {
                StatusCode =
                    ResendUserRegistrationConfirmedEmailResponseStatusCode.SENDING_USER_CONFIRMATION_MAIL_FAIL,
            };
        }

        // Response successfully.
        return new ResendUserRegistrationConfirmedEmailResponse()
        {
            StatusCode = ResendUserRegistrationConfirmedEmailResponseStatusCode.OPERATION_SUCCESS
        };
    }

    /// <summary>
    ///     Sends a confirmation mail to a newly created user.
    /// </summary>
    /// <param name="newUser">
    ///     The newly created user.
    /// </param>
    /// <param name="cancellationToken">
    ///     A cancellation token.
    /// </param>
    /// <returns>
    ///     Returns a task that represents the asynchronous operation. The task result
    ///     contains a value indicating whether the mail was sent successfully.
    /// </returns>
    private async Task<bool> SendingConfirmationMailToUserAsync(
        User newUser,
        CancellationToken cancellationToken
    )
    {
        const string RegistrationConfirmEmailRoutePatuh = "api/auth/sign-up/confirm-email?token=";

        // Init main account creation confirmed email token.
        var accountCreationConfirmEmailToken_1 =
            await _userManager.GenerateEmailConfirmationTokenAsync(user: newUser);

        // Convert to utf-8 byte array.
        var accountCreationConfirmEmailTokenAsBytes_1 = Encoding.UTF8.GetBytes(
            s: $"{accountCreationConfirmEmailToken_1}<token/>{newUser.Id}"
        );

        // Convert to base 64 format.
        var accountCreateConfirmEmailTokenAsBase64_1 = WebEncoders.Base64UrlEncode(
            input: accountCreationConfirmEmailTokenAsBytes_1
        );

        // Init secondary account creation confirmed email token.
        var accountCreationConfirmEmailToken_2 =
            await _userManager.GenerateEmailConfirmationTokenAsync(user: newUser);

        // Init new mail for account confirmation.
        var mailContent = await _sendingMailHandler.GetUserAccountConfirmationMailContentAsync(
            to: newUser.Email,
            subject: "Confirm account registration",
            mainVerifyLink: RegistrationConfirmEmailRoutePatuh
                + accountCreateConfirmEmailTokenAsBase64_1,
            cancellationToken: cancellationToken
        );

        // Send mail to user.
        return await _sendingMailHandler.SendAsync(
            mailContent: mailContent,
            cancellationToken: cancellationToken
        );
    }
}
