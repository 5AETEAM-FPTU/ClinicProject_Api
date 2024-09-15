using Clinic.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Roles" table.
/// </summary>
internal sealed class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(
            name: $"{nameof(Role)}s",
            buildAction: table => table.HasComment(comment: "Contain role records.")
        );

        // Table relationships configurations.
        // [Roles] - [UserRoles] (1 - N).
        builder
            .HasMany(navigationExpression: role => role.UserRoles)
            .WithOne(navigationExpression: userRole => userRole.Role)
            .HasForeignKey(foreignKeyExpression: userRole => userRole.RoleId)
            .IsRequired();

        // [Roles] - [RoleDetails] (1 - 1).
        builder
            .HasOne(navigationExpression: role => role.RoleDetail)
            .WithOne(navigationExpression: roleDetail => roleDetail.Role)
            .HasForeignKey<RoleDetail>(foreignKeyExpression: roleDetail => roleDetail.RoleId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
