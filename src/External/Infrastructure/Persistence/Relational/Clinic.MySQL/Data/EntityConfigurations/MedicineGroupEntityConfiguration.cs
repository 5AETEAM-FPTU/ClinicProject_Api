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

internal sealed class MedicineGroupEntityConfiguration : IEntityTypeConfiguration<MedicineGroup>
{
    public void Configure(EntityTypeBuilder<MedicineGroup> builder)
    {
        builder.ToTable(
                name: $"{nameof(MedicineGroup)}s",
                buildAction: table => table.HasComment(comment: "Contain medicine's group.")
            );

        // Primary key configuration.
        builder.HasKey(keyExpression: medicineGroup => medicineGroup.Id);

        /* Properties configuration */
        // Name
        builder
            .Property(propertyExpression: medicineGroup => medicineGroup.Name)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(36))
            .IsRequired();

        // Constant
        builder
            .Property(propertyExpression: medicineGroup => medicineGroup.Constaint)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(36))
            .IsRequired();

        // CreatedAt, UpdatedAt, RemovedAt
        builder
            .Property(propertyExpression: medicineGroup => medicineGroup.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();
        builder
            .Property(propertyExpression: medicineGroup => medicineGroup.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();
        builder
            .Property(propertyExpression: medicineGroup => medicineGroup.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // Table relationships configurations.
        // [MedicineGroups] - [Medicines] (1 - N).
        builder
            .HasMany(navigationExpression: medicineGroup => medicineGroup.Medicines)
            .WithOne(navigationExpression: medicine => medicine.MedicineGroup)
            .HasForeignKey(foreignKeyExpression: medicine => medicine.MedicineGroupId)
            .IsRequired();
    }
}
