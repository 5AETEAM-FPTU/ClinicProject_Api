using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.MySQL.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorStaffTypes_Users_UserId",
                table: "DoctorStaffTypes");

            migrationBuilder.DropIndex(
                name: "IX_DoctorStaffTypes_UserId",
                table: "DoctorStaffTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DoctorStaffTypes_UserId",
                table: "DoctorStaffTypes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorStaffTypes_Users_UserId",
                table: "DoctorStaffTypes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
