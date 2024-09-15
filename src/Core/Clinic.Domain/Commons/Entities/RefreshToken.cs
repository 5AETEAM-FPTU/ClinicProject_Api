using System;
using Clinic.Domain.Commons.Entities.Base;

namespace Clinic.Domain.Commons.Entities;

/// <summary>
///     Represent the "RefreshTokens" table.
/// </summary>
public class RefreshToken : IBaseEntity
{
    // Primary keys.
    public Guid UserId { get; set; }

    public Guid AccessTokenId { get; set; }

    // Normal columns.
    public string RefreshTokenValue { get; set; }

    public DateTime ExpiredDate { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation properties.
    public User User { get; set; }

    // Additional information of this table.
    public static class MetaData
    {
        public static class RefreshTokenValue
        {
            public const int MinLength = 2;
        }
    }
}
