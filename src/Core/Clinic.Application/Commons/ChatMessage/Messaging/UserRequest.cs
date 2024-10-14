using System;

namespace Clinic.Application.Commons.ChatMessage.Messaging;

/// <summary>
///     Represent the UserRequest model.
/// </summary>
public class UserRequest
{
    public string AvatarUrl { get; set; }

    public string FullName { get; set; }

    public Guid UserId { get; set; }

    public string TitleRequest { get; set; }
}
