using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "RefreshTokens" table.
/// </summary>
internal sealed class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable(
            name: $"{nameof(RefreshToken)}s",
            buildAction: table => table.HasComment(comment: "Contain refresh token records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: refreshToken => refreshToken.AccessTokenId);

        // FullName property configuration
        builder
            .Property(propertyExpression: refreshToken => refreshToken.RefreshTokenValue)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: district => district.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // ExpiredDate property configuration.
        builder
            .Property(propertyExpression: district => district.ExpiredDate)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();
    }
}
