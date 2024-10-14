using System;

namespace Clinic.Application.Commons.Notifier.Messaging;

/// <summary>
///     Represent the UserRequest model.
/// </summary>
public class UserRequest
{
    public string AvatarUrl { get; set; }

    public string FullName { get; set; }

    public Guid UserId { get; set; }

    public string Message { get; set; }
}
