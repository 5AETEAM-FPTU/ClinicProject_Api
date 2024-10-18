using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.MySQL.Migrations
{
    /// <inheritdoc />
    public partial class Migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET FOREIGN_KEY_CHECKS = 0;");

            migrationBuilder.DropIndex(name: "IX_QueueRooms_PatientId", table: "QueueRooms");

            migrationBuilder.CreateIndex(
                name: "IX_QueueRooms_PatientId",
                table: "QueueRooms",
                column: "PatientId"
            );
            migrationBuilder.Sql("SET FOREIGN_KEY_CHECKS = 1;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_QueueRooms_PatientId", table: "QueueRooms");

            migrationBuilder.CreateIndex(
                name: "IX_QueueRooms_PatientId",
                table: "QueueRooms",
                column: "PatientId",
                unique: true
            );
        }
    }
}
