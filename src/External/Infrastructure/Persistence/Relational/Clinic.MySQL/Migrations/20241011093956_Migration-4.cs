using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.MySQL.Migrations
{
    /// <inheritdoc />
    public partial class Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "ServiceOrderItems",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PriceAtOrder",
                table: "ServiceOrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "ServiceOrderItems");

            migrationBuilder.DropColumn(
                name: "PriceAtOrder",
                table: "ServiceOrderItems");
        }
    }
}
