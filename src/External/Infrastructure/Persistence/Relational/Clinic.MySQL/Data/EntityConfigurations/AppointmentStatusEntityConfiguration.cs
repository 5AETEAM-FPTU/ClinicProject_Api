using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "AppointmentStatus" table.
/// </summary>
internal sealed class AppointmentStatusEntityConfiguration
    : IEntityTypeConfiguration<AppointmentStatus>
{
    public void Configure(EntityTypeBuilder<AppointmentStatus> builder)
    {
        builder.ToTable(
            name: $"{nameof(AppointmentStatus)}es",
            buildAction: table => table.HasComment(comment: "Contain appointment status records.")
        );
        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // StatusName property configuration.
        builder
            .Property(propertyExpression: entity => entity.StatusName)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(36))
            .IsRequired();

        // Constant property configuration.
        builder
            .Property(propertyExpression: entity => entity.Constant)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(36))
            .IsRequired();

        // Table relationships configurations.
        // [AppointmentStatus] - [Appointment] (1 - n).
        builder
            .HasMany(navigationExpression: appointmentStatus => appointmentStatus.Appointment)
            .WithOne(navigationExpression: appointment => appointment.AppointmentStatus)
            .HasForeignKey(foreignKeyExpression: appointment => appointment.StatusId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
