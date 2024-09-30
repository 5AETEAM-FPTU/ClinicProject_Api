using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Appointment" table.
/// </summary>
internal sealed class AppointmentEntityConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable(
            name: $"{nameof(Appointment)}s",
            buildAction: table => table.HasComment(comment: "Contain appointment records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // ExaminationDate property configuration.
        builder
            .Property(propertyExpression: entity => entity.ExaminationDate)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // ReExaminationDate property configuration.
        builder.Property(propertyExpression: entity => entity.ReExamination).IsRequired();

        // DepositPayment property configuration.
        builder
            .Property(propertyExpression: entity => entity.DepositPayment)
            .HasDefaultValue(false)
            .IsRequired();

        // Description property configuration.
        builder
            .Property(propertyExpression: entity => entity.Description)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
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

        // [Appointment] - [Feedback] ( 1 - 1)
        builder
            .HasOne(Appointment => Appointment.Feedback)
            .WithOne(Feedback => Feedback.Appointment)
            .HasForeignKey<Feedback>(Feedback => Feedback.AppointmentId);

        builder
            .HasOne(Appointment => Appointment.OnlinePayment)
            .WithOne(OnlinePayment => OnlinePayment.Appointment)
            .HasForeignKey<OnlinePayment>(OnlinePayment => OnlinePayment.AppointmentId);
    }
}
