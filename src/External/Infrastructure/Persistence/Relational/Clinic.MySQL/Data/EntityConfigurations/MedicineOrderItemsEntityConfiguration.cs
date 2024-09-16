using Clinic.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.MySQL.Data.EntityConfigurations;

internal sealed class MedicineOrderItemsEntityConfiguration : IEntityTypeConfiguration<MedicineOrderItems>
{
    public void Configure(EntityTypeBuilder<MedicineOrderItems> builder)
    {
        builder.ToTable(
                name: $"{nameof(MedicineOrderItems)}",
                buildAction: table => table.HasComment(comment: "Contain Medicine Orders record")
            );

        // Primary key configuration.
        builder.HasKey(keyExpression: medicineOrderItem => new
        {
            medicineOrderItem.MedicineId,
            medicineOrderItem.MedicalReportId,
        });

    }
}
