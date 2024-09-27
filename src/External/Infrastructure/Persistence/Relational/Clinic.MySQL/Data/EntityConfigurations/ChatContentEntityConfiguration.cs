using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "ChatContent" table.
/// </summary>
internal sealed class ChatContentEntityConfiguration : IEntityTypeConfiguration<ChatContent>
{
    public void Configure(EntityTypeBuilder<ChatContent> builder)
    {
        builder.ToTable(
            name: $"{nameof(ChatContent)}s",
            buildAction: table => table.HasComment(comment: "Contain chat content records.")
        );
        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // TextContent property configuration.
        builder
            .Property(propertyExpression: entity => entity.TextContent)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // IsRead property configuration.
        builder
            .Property(propertyExpression: entity => entity.IsRead)
            .HasDefaultValue(false)
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
        // [ChatContent] - [Asset] (1 - n).
        builder
            .HasMany(navigationExpression: chatContent => chatContent.Assets)
            .WithOne(navigationExpression: assetContent => assetContent.ChatContent)
            .HasForeignKey(foreignKeyExpression: assetContent => assetContent.ChatContentId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
