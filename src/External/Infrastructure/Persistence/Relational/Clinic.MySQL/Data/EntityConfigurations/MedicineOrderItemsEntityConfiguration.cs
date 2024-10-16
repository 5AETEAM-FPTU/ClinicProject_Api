using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

internal sealed class MedicineOrderItemsEntityConfiguration
    : IEntityTypeConfiguration<MedicineOrderItem>
{
    public void Configure(EntityTypeBuilder<MedicineOrderItem> builder)
    {
        builder.ToTable(
            name: $"{nameof(MedicineOrderItem)}s",
            buildAction: table => table.HasComment(comment: "Contain Medicine Orders record")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: medicineOrderItem => new
        {
            medicineOrderItem.MedicineId,
            medicineOrderItem.MedicineOrderId,                   // fix 16/10/2024
        });

        // Description property configuration.
        builder
            .Property(propertyExpression: medicalReport => medicalReport.Description)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // Description property configuration.
        builder
            .Property(propertyExpression: medicalReport => medicalReport.Quantity)
            .HasDefaultValue(0)
            .IsRequired();
    }
}
