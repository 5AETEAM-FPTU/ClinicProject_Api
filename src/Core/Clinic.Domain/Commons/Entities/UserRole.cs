using System;
using Clinic.Domain.Commons.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "UserRoles" table.
/// </summary>
public class UserRole : IdentityUserRole<Guid>, IBaseEntity
{
    // Navigation properties.
    public User User { get; set; }

    public Role Role { get; set; }
}
