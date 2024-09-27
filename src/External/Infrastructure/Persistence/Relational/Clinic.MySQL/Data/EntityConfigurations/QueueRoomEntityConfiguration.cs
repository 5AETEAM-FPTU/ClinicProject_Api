using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "QueueRoom" table.
/// </summary>
internal sealed class QueueRoomEntityConfiguration : IEntityTypeConfiguration<QueueRoom>
{
    public void Configure(EntityTypeBuilder<QueueRoom> builder)
    {
        builder.ToTable(
            name: $"{nameof(QueueRoom)}s",
            buildAction: table => table.HasComment(comment: "Contain queue room records.")
        );
        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // Message property configuration.
        builder
            .Property(propertyExpression: entity => entity.Message)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // IsSuported property configuration.
        builder
            .Property(propertyExpression: entity => entity.IsSuported)
            .HasDefaultValue(false)
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
    }
}
