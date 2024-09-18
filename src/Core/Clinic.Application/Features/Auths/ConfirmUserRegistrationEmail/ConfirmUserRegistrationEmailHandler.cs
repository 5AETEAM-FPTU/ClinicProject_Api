using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Clinic.Application.Features.Auths.ConfirmUserRegistrationEmail;

/// <summary>
///     ConfirmUserRegistrationEmail request handler.
/// </summary>
internal sealed class ConfirmUserRegistrationEmailHandler
    : IFeatureHandler<ConfirmUserRegistrationEmailRequest, ConfirmUserRegistrationEmailResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ConfirmUserRegistrationEmailHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IWebHostEnvironment webHostEnvironment
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _webHostEnvironment = webHostEnvironment;
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
    public async Task<ConfirmUserRegistrationEmailResponse> ExecuteAsync(
        ConfirmUserRegistrationEmailRequest request,
        CancellationToken cancellationToken
    )
    {
        const string AccountConfirmedUrlIsNotValidTemplateName =
            "AccountConfirmedUrlIsNotValidTemplate.html";
        const string UserHasAlreadyConfirmedAccountSuccessfullyTemplateName =
            "UserHasAlreadyConfirmedAccountSuccessfullyTemplate.html";
        const string UserHasConfirmedAccountSuccessfullyTemplateName =
            "UserHasConfirmedAccountSuccessfullyTemplate.html";
        const string ServerErrorTemplateName = "ServerErrorTemplate.html";

        byte[] decodeAccountCreationConfirmedEmailToken;

        // Decode token base 64.
        try
        {
            decodeAccountCreationConfirmedEmailToken = WebEncoders.Base64UrlDecode(
                input: request.UserRegistrationEmailConfirmedTokenAsBase64
            );
        }
        catch
        {
            return new()
            {
                StatusCode = ConfirmUserRegistrationEmailResponseStatusCode.TOKEN_IS_NOT_CORRECT,
                ResponseBodyAsHtml = await GenerateHtmlResponseAsync(
                    responseTemplateName: AccountConfirmedUrlIsNotValidTemplateName,
                    cancellationToken: cancellationToken
                )
            };
        }

        // Extract decode token.
        var tokens = Encoding
            .UTF8.GetString(bytes: decodeAccountCreationConfirmedEmailToken)
            .Split(separator: "<token/>");

        // Get the user id.
        var userId = Guid.Parse(input: tokens[1]);

        // Find user by user id.
        var foundUser = await _userManager.FindByIdAsync(userId: userId.ToString());

        // Responds if user is not exist
        if (Equals(objA: foundUser, objB: default))
        {
            return new()
            {
                StatusCode = ConfirmUserRegistrationEmailResponseStatusCode.USER_IS_NOT_FOUND,
            };
        }

        // Check if user is not temporarily removed.
        var isUserTemporarilyRemoved =
            await _unitOfWork.ConfirmUserRegistrationEmailRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: foundUser.Id,
                cancellationToken: cancellationToken
            );

        // Responds if user is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    ConfirmUserRegistrationEmailResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        // Has user confirmed account registration email.
        var hasUserConfirmed = await _userManager.IsEmailConfirmedAsync(user: foundUser);

        // Responds if user has confirmed account registration email.
        if (hasUserConfirmed)
        {
            return new()
            {
                StatusCode =
                    ConfirmUserRegistrationEmailResponseStatusCode.USER_HAS_CONFIRMED_REGISTRATION_EMAIL,
                ResponseBodyAsHtml = await GenerateHtmlResponseAsync(
                    responseTemplateName: UserHasAlreadyConfirmedAccountSuccessfullyTemplateName,
                    cancellationToken: cancellationToken
                )
            };
        }

        // Confirm user account registration.
        var dbResult = await _userManager.ConfirmEmailAsync(
            user: foundUser,
            token: tokens[default]
        );

        if (!dbResult.Succeeded)
        {
            return new()
            {
                StatusCode = ConfirmUserRegistrationEmailResponseStatusCode.DATABASE_OPERATION_FAIL,
                ResponseBodyAsHtml = await GenerateHtmlResponseAsync(
                    responseTemplateName: ServerErrorTemplateName,
                    cancellationToken: cancellationToken
                )
            };
        }

        // Response successfully.
        return new ConfirmUserRegistrationEmailResponse()
        {
            StatusCode = ConfirmUserRegistrationEmailResponseStatusCode.OPERATION_SUCCESS,
            ResponseBodyAsHtml = await GenerateHtmlResponseAsync(
                responseTemplateName: UserHasConfirmedAccountSuccessfullyTemplateName,
                cancellationToken: cancellationToken
            )
        };
    }

    private Task<string> GenerateHtmlResponseAsync(
        string responseTemplateName,
        CancellationToken cancellationToken
    )
    {
        var userHasConfirmedAccountSuccessfullyHtmlPath = Path.Combine(
            path1: "CreateUserAccount",
            path2: responseTemplateName
        );

        var fullPath = Path.Combine(
            path1: _webHostEnvironment.WebRootPath,
            path2: userHasConfirmedAccountSuccessfullyHtmlPath
        );

        return File.ReadAllTextAsync(path: fullPath, cancellationToken: cancellationToken);
    }
}
