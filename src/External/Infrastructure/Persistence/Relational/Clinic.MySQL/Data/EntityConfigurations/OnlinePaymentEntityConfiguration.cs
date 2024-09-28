using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "OnlinePayment" table.
/// </summary>
internal sealed class OnlinePaymentEntityConfiguration : IEntityTypeConfiguration<OnlinePayment>
{
    public void Configure(EntityTypeBuilder<OnlinePayment> builder)
    {
        builder.ToTable(
            name: $"{nameof(OnlinePayment)}s",
            buildAction: table => table.HasComment(comment: "Contain online payment records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // Transaction property configuration.
        builder
            .Property(propertyExpression: entity => entity.TransactionID)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(100))
            .IsRequired();

        // PaymentMethod property configuration.
        builder
            .Property(propertyExpression: entity => entity.PaymentMethod)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(100))
            .IsRequired();

        // Amount property configuration.
        builder.Property(propertyExpression: entity => entity.Amount).IsRequired();

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

        // Table relationships configurations.
        // [OnlinePayment] - [Appointment] (1 - 1).
        builder
            .HasOne(navigationExpression: onlinePayment => onlinePayment.Appointment)
            .WithOne(navigationExpression: appointment => appointment.OnlinePayment)
            .HasForeignKey<Appointment>(foreignKeyExpression: appointment =>
                appointment.OnlinePaymentId
            )
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
