using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Doctors" table.
/// </summary>
internal sealed class DoctorEntityConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable(
            name: $"{nameof(Doctor)}s",
            buildAction: table => table.HasComment(comment: "Contain doctor records.")
        );
        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.UserId);

        // DOB property configuration.
        builder
            .Property(propertyExpression: entity => entity.DOB)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // Address property configuration.
        builder
            .Property(propertyExpression: entity => entity.Address)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(255))
            .IsRequired();

        // Description property configuration.
        builder
            .Property(propertyExpression: entity => entity.Description)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // Achievement property configuration.
        builder
            .Property(propertyExpression: entity => entity.Achievement)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // IsOnDuty property configuration.
        builder
            .Property(propertyExpression: entity => entity.IsOnDuty)
            .HasDefaultValue(false)
            .IsRequired();

        // Table relationships configurations.
        // [Doctor] - [Schedule] (1 - N).
        builder
            .HasMany(navigationExpression: doctor => doctor.Schedules)
            .WithOne(navigationExpression: schedule => schedule.Doctor)
            .HasForeignKey(foreignKeyExpression: schedule => schedule.DoctorId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction)
            .IsRequired();

        // [Doctor] - [ChatRoom] (1 - N).
        builder
            .HasMany(navigationExpression: doctor => doctor.ChatRooms)
            .WithOne(navigationExpression: doctorSpecialties => doctorSpecialties.Doctor)
            .HasForeignKey(foreignKeyExpression: doctorSpecialties => doctorSpecialties.DoctorId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction)
            .IsRequired();

        // [Doctor] - [DoctorSpecialty] (1 - N).
        builder
            .HasMany(navigationExpression: doctor => doctor.DoctorSpecialties)
            .WithOne(navigationExpression: doctorSpecialties => doctorSpecialties.Doctor)
            .HasForeignKey(foreignKeyExpression: doctorSpecialties => doctorSpecialties.DoctorId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction)
            .IsRequired();
    }
}
