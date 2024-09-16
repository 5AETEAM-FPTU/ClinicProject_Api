using Clinic.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Clinic.MySQL.Common;


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
        builder.HasKey(keyExpression: entity => entity.Id);

        // Gender property configuration
        builder
            .Property(propertyExpression: entity => entity.Gender)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(10))
            .IsRequired();

        // DOB property configuration.
        builder
            .Property(propertyExpression: entity => entity.DOB)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // Position property configuration.
        builder
            .Property(propertyExpression: entity => entity.Position)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(100))
            .IsRequired();

        // Specialty property configuration.
        builder
            .Property(propertyExpression: entity => entity.Specialty)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(225))
            .IsRequired();

        // Address property configuration.
        builder
            .Property(propertyExpression: entity => entity.Address)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(225))
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

        // Table relationships configurations.
        // [Doctor] - [WorkingHour] (1 - N).
        builder
            .HasMany(navigationExpression: doctor => doctor.WorkingHours)
            .WithOne(navigationExpression: workingHour => workingHour.Doctor)
            .HasForeignKey(foreignKeyExpression: workingHour => workingHour.DoctorId)
            .IsRequired();

        // [Doctor] - [ChatRoom] (1 - N).
        builder
            .HasMany(navigationExpression: doctor => doctor.ChatRooms)
            .WithOne(navigationExpression: chatRoom => chatRoom.Doctor)
            .HasForeignKey(foreignKeyExpression: chatRoom => chatRoom.DoctorId)
            .IsRequired();
    }
}
