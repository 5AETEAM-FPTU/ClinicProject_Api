using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

internal sealed class MedicineTypeEntityConfiguration : IEntityTypeConfiguration<MedicineType>
{
    public void Configure(EntityTypeBuilder<MedicineType> builder)
    {
        builder.ToTable(
            name: $"{nameof(MedicineType)}s",
            buildAction: table => table.HasComment(comment: "Contain medicine's type.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: medicineType => medicineType.Id);

        /* Properties configuration */
        // Name
        builder
            .Property(propertyExpression: medicineType => medicineType.Name)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(36))
            .IsRequired();

        // Constant
        builder
            .Property(propertyExpression: medicineType => medicineType.Constant)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(36))
            .IsRequired();

        // Table relationships configurations.
        // [MedicineTypes] - [Medicines] (1 - N).
        builder
            .HasMany(navigationExpression: medicineType => medicineType.Medicines)
            .WithOne(navigationExpression: medicine => medicine.MedicineType)
            .HasForeignKey(foreignKeyExpression: medicine => medicine.MedicineTypeId)
            .IsRequired();
    }
}
