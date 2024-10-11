using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Feedback" table.
/// </summary>
internal sealed class FeedbackEntityConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable(
            name: $"{nameof(Feedback)}s",
            buildAction: table => table.HasComment(comment: "Contain Feedback records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // Comment property configuration.
        builder
            .Property(propertyExpression: entity => entity.Comment)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(255))
            .IsRequired();

        // Vote property configuration.
        builder
            .Property(propertyExpression: entity => entity.Vote)
            .HasDefaultValue(0)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: entity => entity.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder.Property(propertyExpression: entity => entity.CreatedBy).IsRequired();
    }
}
