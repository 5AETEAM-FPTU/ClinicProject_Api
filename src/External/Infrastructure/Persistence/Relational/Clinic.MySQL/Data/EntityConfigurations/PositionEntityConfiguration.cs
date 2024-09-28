using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Position" table.
/// </summary>
internal sealed class PositionEntityConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable(
            name: $"{nameof(Position)}s",
            buildAction: table => table.HasComment(comment: "Contain doctor status records.")
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
        // [Position] - [Doctors] (1 - n).
        builder
            .HasMany(navigationExpression: position => position.Doctors)
            .WithOne(navigationExpression: doctor => doctor.Position)
            .HasForeignKey(foreignKeyExpression: doctor => doctor.PositionId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
