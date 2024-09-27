using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

internal sealed class ServiceOrderItemsEntityConfiguration
    : IEntityTypeConfiguration<ServiceOrderItem>
{
    public void Configure(EntityTypeBuilder<ServiceOrderItem> builder)
    {
        builder.ToTable(
            name: $"{nameof(ServiceOrderItem)}s",
            buildAction: table => table.HasComment(comment: "Contain Service Orders record")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: serviceOrderItem => new
        {
            serviceOrderItem.ServiceId,
            serviceOrderItem.ServiceOrderId,
        });

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: service => service.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: service => service.CreatedBy).IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: service => service.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder.Property(propertyExpression: service => service.UpdatedBy).IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: service => service.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: service => service.RemovedBy).IsRequired();
    }
}
