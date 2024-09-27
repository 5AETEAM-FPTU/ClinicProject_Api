using Clinic.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CommonConstant = Clinic.MySQL.Common.CommonConstant;

namespace Clinic.MySQL.Data.EntityConfigurations;

internal sealed class GenderEntityConfiguration : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        builder.ToTable(
            name: $"{nameof(Gender)}s",
            buildAction: table => table.HasComment(comment: "Contain Gender records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: gender => gender.Id);

        // Name property configuration.
        builder
            .Property(propertyExpression: entity => entity.Name)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(36))
            .IsRequired();

        // Constant property configuration.
        builder
            .Property(propertyExpression: entity => entity.Constant)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(36))
            .IsRequired();

        // Table relationships configurations.
        // [Genders] - [Doctors] (1 - n).
        builder
            .HasMany(navigationExpression: gender => gender.Users)
            .WithOne(navigationExpression: user => user.Gender)
            .HasForeignKey(foreignKeyExpression: user => user.GenderId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
