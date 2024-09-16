using Clinic.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.MySQL.Data.EntityConfigurations;

internal sealed class ServiceOrderItemsEntityConfiguration : IEntityTypeConfiguration<ServiceOrderItems>
{
    public void Configure(EntityTypeBuilder<ServiceOrderItems> builder)
    {
        builder.ToTable(
                name: $"{nameof(ServiceOrderItems)}",
                buildAction: table => table.HasComment(comment: "Contain Service Orders record")
            );

        // Primary key configuration.
        builder.HasKey(keyExpression: serviceOrderItem => new
        {
            serviceOrderItem.ServiceId,
            serviceOrderItem.MedicalReportId,
        });

    }
}
