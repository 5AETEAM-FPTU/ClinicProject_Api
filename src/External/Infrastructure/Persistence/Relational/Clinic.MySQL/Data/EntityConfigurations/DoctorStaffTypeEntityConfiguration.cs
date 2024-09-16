using Clinic.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Clinic.MySQL.Common;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "DoctorStaffTypes" table.
/// </summary>
internal sealed class DoctorStaffTypeEntityConfiguration : IEntityTypeConfiguration<DoctorStaffType>
{
    public void Configure(EntityTypeBuilder<DoctorStaffType> builder)
    {
        builder.ToTable(
            name: $"{nameof(DoctorStaffType)}s",
            buildAction: table => table.HasComment(comment: "Contain doctor staff types records.")
        );
        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // TypeName property configuration
        builder
            .Property(propertyExpression: entity => entity.TypeName)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(36))
            .IsRequired();

        // Constant property configuration.
        builder
            .Property(propertyExpression: entity => entity.Constant)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(36))
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: entity => entity.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: entity => entity.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: entity => entity.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: entity => entity.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: entity => entity.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: entity => entity.RemovedBy).IsRequired();

        // Table relationships configurations.
        // [DoctorStaffType] - [Doctor] (1 - N).
        builder
            .HasMany(navigationExpression: doctorStaffType => doctorStaffType.Doctors)
            .WithOne(navigationExpression: doctor => doctor.DoctorStaffType)
            .HasForeignKey(foreignKeyExpression: doctor => doctor.DoctorStaffTypeId)
            .IsRequired();

    }
}
