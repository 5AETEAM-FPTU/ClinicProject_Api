using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

internal sealed class MedicalReportEntityConfiguration : IEntityTypeConfiguration<MedicalReport>
{
    public void Configure(EntityTypeBuilder<MedicalReport> builder)
    {
        builder.ToTable(
            name: $"{nameof(MedicalReport)}s",
            buildAction: table => table.HasComment(comment: "Contain medical report records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: medicalReport => medicalReport.Id);

        // Name property configuration.
        builder
            .Property(propertyExpression: medicalReport => medicalReport.Name)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(100))
            .IsRequired();

        // MedicalHistory property configuration.
        builder
            .Property(propertyExpression: medicalReport => medicalReport.MedicalHistory)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(256))
            .IsRequired();

        // TotalPrice property configuration.
        builder
            .Property(propertyExpression: medicalReport => medicalReport.TotalPrice)
            .HasDefaultValue(0)
            .IsRequired();

        // GeneralCondition property configuration.
        builder
            .Property(propertyExpression: medicalReport => medicalReport.GeneralCondition)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(256))
            .IsRequired();

        // Weight property configuration.
        builder
            .Property(propertyExpression: medicalReport => medicalReport.Weight)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(50))
            .IsRequired();

        // Height property configuration.
        builder
            .Property(propertyExpression: medicalReport => medicalReport.Height)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(50))
            .IsRequired();

        // Pulse property configuration.
        builder
            .Property(propertyExpression: medicalReport => medicalReport.Pulse)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(50))
            .IsRequired();

        // Temperature property configuration.
        builder
            .Property(propertyExpression: medicalReport => medicalReport.Temperature)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(50))
            .IsRequired();

        // BloodPresser property configuration.
        builder
            .Property(propertyExpression: medicalReport => medicalReport.BloodPresser)
            .IsRequired();

        // Diagnosis property configuration.
        builder
            .Property(propertyExpression: medicalReport => medicalReport.Diagnosis)
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
    }
}
