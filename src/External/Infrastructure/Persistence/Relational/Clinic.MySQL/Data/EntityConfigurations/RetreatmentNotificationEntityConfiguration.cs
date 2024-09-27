using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "RetreatmentNotification" table.
/// </summary>
internal sealed class RetreatmentNotificationEntityConfiguration
    : IEntityTypeConfiguration<RetreatmentNotification>
{
    public void Configure(EntityTypeBuilder<RetreatmentNotification> builder)
    {
        builder.ToTable(
            name: $"{nameof(RetreatmentNotification)}s",
            buildAction: table =>
                table.HasComment(comment: "Contain RetreatmentNotification status records.")
        );
        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // ExaminationDate property configuration.
        builder
            .Property(propertyExpression: entity => entity.ExaminationDate)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();
    }
}
