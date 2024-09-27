using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Schedules" table.
/// </summary>
internal sealed class ScheduleEntityConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable(
            name: $"{nameof(Schedule)}s",
            buildAction: table => table.HasComment(comment: "Contain Schedule records.")
        );
        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // StartDate property configuration.
        builder
            .Property(propertyExpression: entity => entity.StartDate)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // EndDate property configuration.
        builder
            .Property(propertyExpression: entity => entity.EndDate)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: entity => entity.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
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

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: entity => entity.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder.Property(propertyExpression: entity => entity.RemovedBy).IsRequired();

        // Table relationships configurations.
        // [WorkingHour] - [Appointment] (1 - 1).
        builder
            .HasOne(navigationExpression: workingHour => workingHour.Appointment)
            .WithOne(navigationExpression: appointment => appointment.Schedule)
            .HasForeignKey<Appointment>(foreignKeyExpression: appointment => appointment.ScheduleId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
