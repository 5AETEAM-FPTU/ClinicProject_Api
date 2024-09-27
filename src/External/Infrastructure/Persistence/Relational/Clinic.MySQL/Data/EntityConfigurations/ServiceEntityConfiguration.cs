using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

internal sealed class ServiceEntityConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable(
            name: $"{nameof(Service)}s",
            buildAction: table => table.HasComment(comment: "Contain service.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: service => service.Id);

        /* Properties configuration */
        // Code
        builder
            .Property(propertyExpression: service => service.Code)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(50))
            .IsRequired();

        // Name
        builder
            .Property(propertyExpression: service => service.Name)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(100))
            .IsRequired();

        // Group
        builder
            .Property(propertyExpression: service => service.Group)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(50))
            .IsRequired();

        // Description
        builder
            .Property(propertyExpression: service => service.Descripiton)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

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

        // Table relationships configurations.
        // [Services] - [ServiceOrderItems] (1 - N).
        builder
            .HasMany(navigationExpression: service => service.ServiceOrderItems)
            .WithOne(navigationExpression: serviceOrderItem => serviceOrderItem.Service)
            .HasForeignKey(foreignKeyExpression: serviceOrderItem => serviceOrderItem.ServiceId)
            .IsRequired();
    }
}
