using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            .Property(propertyExpression: medicineGroup => medicineGroup.Constant)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(36))
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
