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

internal sealed class MedicineEntityConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.ToTable(
                name: $"{nameof(Medicine)}s",
                buildAction: table => table.HasComment(comment: "Contain medicine's infomation.")
            );

        // Primary key configuration.
        builder.HasKey(keyExpression: medicine => medicine.Id);

        /* Properties configuration */
        // Name
        builder
            .Property(propertyExpression: medicine => medicine.Name)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(100))
            .IsRequired();

        // Ingredient
        builder
            .Property(propertyExpression: medicine => medicine.Ingredient)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(100))
            .IsRequired();

        // Manufacture
        builder
            .Property(propertyExpression: medicine => medicine.Manufacture)
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
        // [Medicines] - [MedicineOrderItems] (1 - n).
        builder
            .HasMany(navigationExpression: medicine => medicine.MedicineOrderItems)
            .WithOne(navigationExpression: medicineOrderItem => medicineOrderItem.Medicine)
            .HasForeignKey(foreignKeyExpression: medicineOrderItem => medicineOrderItem.MedicineId)
            .IsRequired();

    }
}
