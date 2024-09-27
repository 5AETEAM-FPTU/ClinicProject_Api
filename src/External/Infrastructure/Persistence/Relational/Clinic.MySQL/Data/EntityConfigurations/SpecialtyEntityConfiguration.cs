using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Specialty" table.
/// </summary>
internal sealed class SpecialtyEntityConfiguration : IEntityTypeConfiguration<Specialty>
{
    public void Configure(EntityTypeBuilder<Specialty> builder)
    {
        builder.ToTable(
            name: $"{nameof(Specialty)}s",
            buildAction: table =>
                table.HasComment(comment: "Contain doctorSpecialties status records.")
        );
        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // StatusName property configuration.
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
        // [Specialty] - [DoctorSpecialties] (1 - n).
        builder
            .HasMany(navigationExpression: specialty => specialty.DoctorSpecialties)
            .WithOne(navigationExpression: doctorSpecialties => doctorSpecialties.Specialty)
            .HasForeignKey(foreignKeyExpression: doctorSpecialties => doctorSpecialties.SpecialtyID)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
