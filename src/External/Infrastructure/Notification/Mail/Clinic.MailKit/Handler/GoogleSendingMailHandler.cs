using System.Text;
using Clinic.Application.Commons.Mail;
using Clinic.Configuration.Infrastructure.Mail.Google;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using MimeKit;

namespace Clinic.MailKit.Handler;

/// <summary>
///     Implementation of sending mail handler interface.
/// </summary>
internal sealed class GoogleSendingMailHandler : ISendingMailHandler
{
    private readonly GoogleGmailSendingOption _googleGmailSendingOption;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GoogleSendingMailHandler(
        GoogleGmailSendingOption googleGmailSendingOption,
        IWebHostEnvironment webHostEnvironment
    )
    {
        _googleGmailSendingOption = googleGmailSendingOption;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<AppMailContent> GetUserAccountConfirmationMailContentAsync(
        string to,
        string subject,
        string mainVerifyLink,
        CancellationToken cancellationToken
    )
    {
        if (
            string.IsNullOrWhiteSpace(value: to)
            || string.IsNullOrWhiteSpace(value: subject)
            || string.IsNullOrWhiteSpace(value: mainVerifyLink)
        )
        {
            return default;
        }

        var mailTemplatePath = Path.Combine(
            path1: "CreateUserAccount",
            path2: "AskUserToConfirmedAccountMailTemplate.html"
        );

        var htmlTemplate = await ReadTemplateAsync(
            templatePath: mailTemplatePath,
            cancellationToken: cancellationToken
        );

        var mailBody = new StringBuilder(value: htmlTemplate)
            .Replace(
                oldValue: "{verify-link}",
                newValue: _googleGmailSendingOption.WebUrl + mainVerifyLink
            )
            .Replace(oldValue: "{username}", newValue: "!")
            .ToString();

        return new()
        {
            To = to,
            Subject = subject,
            Body = mailBody
        };

        async Task<string> ReadTemplateAsync(
            string templatePath,
            CancellationToken cancellationToken
        )
        {
            var templateFilePath = Path.Combine(_webHostEnvironment.WebRootPath, templatePath);

            return await File.ReadAllTextAsync(templateFilePath, cancellationToken);
        }
    }

    public async Task<AppMailContent> GetUserResetPasswordMailContentAsync(
        string to,
        string subject,
        string resetPasswordToken,
        CancellationToken cancellationToken
    )
    {
        if (
            string.IsNullOrWhiteSpace(value: to)
            || string.IsNullOrWhiteSpace(value: subject)
            || string.IsNullOrEmpty(resetPasswordToken)
        )
        {
            return default;
        }

        var mailTemplatePath = Path.Combine(
            path1: "CreateUserAccount",
            path2: "UserResetPasswordHtmlTemplate.html"
        );

        var htmlTemplate = await ReadTemplateAsync(
            templatePath: mailTemplatePath,
            cancellationToken: cancellationToken
        );

        var mailBody = new StringBuilder(value: htmlTemplate)
            .Replace(oldValue: "{passcode}", newValue: resetPasswordToken)
            .ToString();

        return new()
        {
            To = to,
            Subject = subject,
            Body = mailBody
        };

        async Task<string> ReadTemplateAsync(
            string templatePath,
            CancellationToken cancellationToken
        )
        {
            var templateFilePath = Path.Combine(_webHostEnvironment.WebRootPath, templatePath);

            return await File.ReadAllTextAsync(templateFilePath, cancellationToken);
        }
    }

    public async Task<bool> SendAsync(
        AppMailContent mailContent,
        CancellationToken cancellationToken
    )
    {
        if (Equals(objA: mailContent, objB: default))
        {
            return false;
        }

        MimeMessage email =
            new()
            {
                Sender = new(
                    name: _googleGmailSendingOption.DisplayName,
                    address: _googleGmailSendingOption.Mail
                ),
                From =
                {
                    new MailboxAddress(
                        name: _googleGmailSendingOption.DisplayName,
                        address: _googleGmailSendingOption.Mail
                    )
                },
                To = { MailboxAddress.Parse(text: mailContent.To) },
                Subject = mailContent.Subject,
                Body = new BodyBuilder { HtmlBody = mailContent.Body }.ToMessageBody()
            };
        try
        {
            using SmtpClient smtp = new();

            await smtp.ConnectAsync(
                host: _googleGmailSendingOption.Host,
                port: _googleGmailSendingOption.Port,
                options: SecureSocketOptions.StartTlsWhenAvailable,
                cancellationToken: cancellationToken
            );

            await smtp.AuthenticateAsync(
                userName: _googleGmailSendingOption.Mail,
                password: _googleGmailSendingOption.Password,
                cancellationToken: cancellationToken
            );

            await smtp.SendAsync(message: email, cancellationToken: cancellationToken);

            await smtp.DisconnectAsync(quit: true, cancellationToken: cancellationToken);
        }
        catch
        {
            return false;
        }

        return true;
    }
}
