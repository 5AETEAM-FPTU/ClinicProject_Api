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

        // CreatedAt, UpdatedAt, RemovedAt
        builder
            .Property(propertyExpression: service => service.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();
        builder
            .Property(propertyExpression: service => service.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();
        builder
            .Property(propertyExpression: service => service.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // Table relationships configurations.
        // [Services] - [ServiceOrderItems] (1 - N).
        builder
            .HasMany(navigationExpression: service => service.ServiceOrderItems)
            .WithOne(navigationExpression: serviceOrderItem => serviceOrderItem.Service)
            .HasForeignKey(foreignKeyExpression: serviceOrderItem => serviceOrderItem.ServiceId)
            .IsRequired();
    }
}
