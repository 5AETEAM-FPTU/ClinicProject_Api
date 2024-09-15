using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.MySQL.Data.EntityConfigurations;

/// <summary>
///     Represent "UsersToken" table configuration.
/// </summary>
internal sealed class UserTokenEntityConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        const string TableName = "UserTokens";
        const string TableComment = "Contain user record.";

        builder.ToTable(name: TableName, buildAction: table => table.HasComment(TableComment));

        // ExpiredAt property configuration.
        builder
            .Property(propertyExpression: userToken => userToken.ExpiredAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();
    }
}
