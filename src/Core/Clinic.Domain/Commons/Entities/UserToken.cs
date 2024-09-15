using System;
using Clinic.Domain.Commons.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "UserTokens" table.
/// </summary>
public class UserToken : IdentityUserToken<Guid>, IBaseEntity
{
    public DateTime ExpiredAt { get; set; }

    // Navigation properties.
    public User User { get; set; }
}
