using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Application.Commons.Mail;

/// <summary>
///     Represent interface of sending mail handler.
/// </summary>
public interface ISendingMailHandler
{
    /// <summary>
    ///     Get user account confirmation mail content.
    /// </summary>
    /// <param name="to">
    ///     Send to whom.
    /// </param>
    /// <param name="subject">
    ///     Mail subject
    /// </param>
    /// <param name="mainVerifyLink">
    ///     Main mail verification link.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Model contain receiver information.
    /// </returns>
    Task<AppMailContent> GetUserAccountConfirmationMailContentAsync(
        string to,
        string subject,
        string mainVerifyLink,
        CancellationToken cancellationToken
    );

    /// <summary>
    ///     Get user account confirmation mail content.
    /// </summary>
    /// <param name="to">
    ///     Send to whom.
    /// </param>
    /// <param name="subject">
    ///     Mail subject
    /// </param>
    /// <param name="resetPasswordToken">
    ///     Mail reset password token.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Model contain receiver information.
    /// </returns>
    Task<AppMailContent> GetUserResetPasswordMailContentAsync(
        string to,
        string subject,
        string resetPasswordToken,
        CancellationToken cancellationToken
    );

    /// <summary>
    ///     Sending an email to the specified user.
    /// </summary>
    /// <param name="mailContent">
    ///     A model contains all receiver information.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Task containing boolean result.
    /// </returns>
    Task<bool> SendAsync(AppMailContent mailContent, CancellationToken cancellationToken);
}
