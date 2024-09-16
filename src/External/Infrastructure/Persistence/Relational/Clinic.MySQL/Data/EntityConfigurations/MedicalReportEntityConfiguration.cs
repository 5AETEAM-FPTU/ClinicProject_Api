using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /* Properties configuration */
        // Code
        builder
            .Property(propertyExpression: medicalReport => medicalReport.Code)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(50))
            .IsRequired();

        // Name
        builder
            .Property(propertyExpression: medicalReport => medicalReport.Name)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(100))
            .IsRequired();

        // CreatedAt, UpdatedAt, RemovedAt
        builder
            .Property(propertyExpression: medicalReport => medicalReport.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();
        builder
            .Property(propertyExpression: medicalReport => medicalReport.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();
        builder
            .Property(propertyExpression: medicalReport => medicalReport.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // Table relationships configurations.
        // [MedicalReport] - [ServiceOrderItems] (1 - N).
        builder
            .HasMany(navigationExpression: medicalReport => medicalReport.ServiceOrderItems)
            .WithOne(navigationExpression: serviceOrderItem => serviceOrderItem.MedicalReport)
            .HasForeignKey(foreignKeyExpression: serviceOrderItem => serviceOrderItem.MedicalReportId)
            .IsRequired();

        // [MedicalReport] - [MedicineOrderItems] (1 - N).
        builder
            .HasMany(navigationExpression: medicalReport => medicalReport.MedicineOrderItems)
            .WithOne(navigationExpression: medicineOrderItem => medicineOrderItem.MedicalReport)
            .HasForeignKey(foreignKeyExpression: medicineOrderItem => medicineOrderItem.MedicalReportId)
            .IsRequired();

        // [MedicalReport] - [Appointment] (1 - 1).
        builder
            .HasOne(navigationExpression: medicalReport => medicalReport.Appointment)
            .WithOne(navigationExpression: appointment => appointment.MedicalReport)
            .HasForeignKey<Appointment>(appointment => appointment.MedicalReportId)
            .IsRequired();
    }
}
