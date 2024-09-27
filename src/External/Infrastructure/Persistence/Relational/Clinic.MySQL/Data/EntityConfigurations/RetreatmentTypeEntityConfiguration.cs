using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "RetreatmentType" table.
/// </summary>
internal sealed class RetreatmentTypeEntityConfiguration : IEntityTypeConfiguration<RetreatmentType>
{
    public void Configure(EntityTypeBuilder<RetreatmentType> builder)
    {
        builder.ToTable(
            name: $"{nameof(RetreatmentType)}s",
            buildAction: table =>
                table.HasComment(comment: "Contain RetreatmentType status records.")
        );
        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // StatusName property configuration.
        builder
            .Property(propertyExpression: entity => entity.Name)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(36))
            .IsRequired();

        // Constant property configuration.
        builder
            .Property(propertyExpression: entity => entity.Constant)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(36))
            .IsRequired();

        // Table relationships configurations.
        // [RetreatmentType] - [RetreatmentNotifications] (1 - n).
        builder
            .HasMany(navigationExpression: retreatmentType =>
                retreatmentType.RetreatmentNotifications
            )
            .WithOne(navigationExpression: retreatmentNotification =>
                retreatmentNotification.RetreatmentType
            )
            .HasForeignKey(foreignKeyExpression: retreatmentNotification =>
                retreatmentNotification.RetreatmentTypeId
            )
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
