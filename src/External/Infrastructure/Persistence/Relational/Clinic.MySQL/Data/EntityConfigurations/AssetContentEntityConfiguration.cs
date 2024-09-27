using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "AssetContent" table.
/// </summary>
internal sealed class AssetContentEntityConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable(
            name: $"{nameof(Asset)}s",
            buildAction: table => table.HasComment(comment: "Contain asset content records.")
        );
        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // FileName property configuration.
        builder
            .Property(propertyExpression: entity => entity.FileName)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // FilePath property configuration.
        builder
            .Property(propertyExpression: entity => entity.FilePath)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // Type property configuration.
        builder
            .Property(propertyExpression: entity => entity.Type)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(40))
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: entity => entity.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: entity => entity.RemovedBy).IsRequired();
    }
}
