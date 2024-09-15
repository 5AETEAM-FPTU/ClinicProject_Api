namespace Clinic.Configuration.Infrastructure.Mail.Google;

/// summary
///     The GoogleGmailSendingOption class is used to hold various mail configuration settings.
/// summary
public sealed class GoogleGmailSendingOption
{
    public string Mail { get; set; }

    public string DisplayName { get; set; }

    public string Password { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }

    public string WebUrl { get; set; }
}
