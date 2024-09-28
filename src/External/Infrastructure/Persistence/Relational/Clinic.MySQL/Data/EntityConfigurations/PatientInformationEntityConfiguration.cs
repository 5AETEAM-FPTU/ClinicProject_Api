using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "PatientInformation" table.
/// </summary>
internal sealed class PatientInformationEntityConfiguration
    : IEntityTypeConfiguration<PatientInformation>
{
    public void Configure(EntityTypeBuilder<PatientInformation> builder)
    {
        builder.ToTable(
            name: $"{nameof(PatientInformation)}s",
            buildAction: table => table.HasComment(comment: "Contain PatientInformation records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // FullName property configuration.
        builder
            .Property(propertyExpression: entity => entity.FullName)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(255))
            .IsRequired();

        // Gender property configuration.
        builder
            .Property(propertyExpression: entity => entity.Gender)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(255))
            .IsRequired();

        // DOB property configuration.
        builder
            .Property(propertyExpression: entity => entity.DOB)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // Address property configuration.
        builder
            .Property(propertyExpression: entity => entity.Address)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(255))
            .IsRequired();

        // PhoneNumber property configuration.
        builder
            .Property(propertyExpression: entity => entity.PhoneNumber)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(12))
            .IsRequired();

        // [PatientInformation] - [MedicalReport] (1 - 1)
        builder
            .HasOne(entity => entity.MedicalReport)
            .WithOne(medicalReport => medicalReport.PatientInformation)
            .HasForeignKey<MedicalReport>(medicalReport => medicalReport.PatientInformationId);
    }
}
