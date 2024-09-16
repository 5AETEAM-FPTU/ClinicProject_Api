using Clinic.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.MySQL.Common;

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
        // [ChatContent] - [AssetContent] (1 - 1).
        builder
            .HasOne(navigationExpression: chatContent => chatContent.AssetContent)
            .WithOne(navigationExpression: assetContent => assetContent.ChatContent)
            .HasForeignKey<AssetContent>(foreignKeyExpression: assetContent => assetContent.ChatContentId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
