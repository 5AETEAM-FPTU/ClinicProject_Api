using Clinic.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Users" table.
/// </summary>
internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(
            name: $"{nameof(User)}s",
            buildAction: table => table.HasComment(comment: "Contain user records.")
        );

        // Table relationships configurations.
        // [Users] - [UserRoles] (1 - N).
        builder
            .HasMany(navigationExpression: user => user.UserRoles)
            .WithOne(navigationExpression: userRole => userRole.User)
            .HasForeignKey(foreignKeyExpression: userRole => userRole.UserId)
            .IsRequired();

        // [Users] - [UserTokens] (1 - N).
        builder
            .HasMany(navigationExpression: user => user.UserTokens)
            .WithOne(navigationExpression: userToken => userToken.User)
            .HasForeignKey(foreignKeyExpression: userToken => userToken.UserId)
            .IsRequired();

        // [Users] - [ChatContent] (1 - N).
        builder
            .HasMany(navigationExpression: user => user.ChatContents)
            .WithOne(navigationExpression: chatContent => chatContent.User)
            .HasForeignKey(foreignKeyExpression: chatContent => chatContent.SenderId)
            .IsRequired();

        // [Users] - [Doctor] (1 - 1).
        builder
            .HasOne(navigationExpression: user => user.Doctor)
            .WithOne(navigationExpression: doctor => doctor.User)
            .HasForeignKey<Doctor>(foreignKeyExpression: doctor => doctor.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        // [Users] - [Patient] (1 - 1).
        builder
            .HasOne(navigationExpression: user => user.Patient)
            .WithOne(navigationExpression: patient => patient.User)
            .HasForeignKey<Patient>(foreignKeyExpression: patient => patient.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
