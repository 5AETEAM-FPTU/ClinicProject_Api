using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "ChatRoom" table.
/// </summary>
internal sealed class ChatRoomEntityConfiguration : IEntityTypeConfiguration<ChatRoom>
{
    public void Configure(EntityTypeBuilder<ChatRoom> builder)
    {
        builder.ToTable(
            name: $"{nameof(ChatRoom)}s",
            buildAction: table => table.HasComment(comment: "Contain chat room records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: entity => entity.Id);

        // LastMessage property configuration.
        builder
            .Property(propertyExpression: entity => entity.LastMessage)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(256))
            .IsRequired();

        // IsEnd property configuration.
        builder
            .Property(propertyExpression: entity => entity.IsEnd)
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

        // Table relationships configurations.
        // [ChatRoom] - [ChatContent] (1 - N).
        builder
            .HasMany(navigationExpression: chatRoom => chatRoom.ChatContents)
            .WithOne(navigationExpression: chatContent => chatContent.ChatRoom)
            .HasForeignKey(foreignKeyExpression: chatContent => chatContent.ChatRoomId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction)
            .IsRequired();
    }
}
