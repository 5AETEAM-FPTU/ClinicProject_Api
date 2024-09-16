using System;
using System.Collections.Generic;
using Clinic.Domain.Commons.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "Users" table.
/// </summary>
public class User : IdentityUser<Guid>, IBaseEntity
{
    // Navigation properties.

    // Navigation collections.
    public IEnumerable<UserRole> UserRoles { get; set; }

    // Navigation collections.
    public IEnumerable<UserToken> UserTokens { get; set; }

    // Additional information of this table.
    public static class MetaData
    {
        public static class Email
        {
            public const int MaxLength = 256;

            public const int MinLength = 2;
        }

        public static class UserName
        {
            public const int MaxLength = 256;

            public const int MinLength = 2;
        }

        public static class Password
        {
            public const int MinLength = 4;

            public const int MaxLength = 256;
        }

        public static class Phone
        {
            public const int MinLength = 10;

            public const int MaxLength = 11;
        }
    }
    //Navigation properties.
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
    public IEnumerable<ChatContent> ChatContents { get; set; }
}
