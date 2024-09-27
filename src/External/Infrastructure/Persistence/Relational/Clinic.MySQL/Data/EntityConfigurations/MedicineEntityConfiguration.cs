using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        // [Medicines] - [MedicineOrderItems] (1 - n).
        builder
            .HasMany(navigationExpression: medicine => medicine.MedicineOrderItems)
            .WithOne(navigationExpression: medicineOrderItem => medicineOrderItem.Medicine)
            .HasForeignKey(foreignKeyExpression: medicineOrderItem => medicineOrderItem.MedicineId)
            .IsRequired();
    }
}
