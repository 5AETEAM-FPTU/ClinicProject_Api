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
            migrationBuilder.DropForeignKey(
                name: "FK_QueueRooms_Patients_PatientId",
                table: "QueueRooms");

            migrationBuilder.DropIndex(
                name: "IX_QueueRooms_PatientId",
                table: "QueueRooms");

            migrationBuilder.CreateIndex(
                name: "IX_QueueRooms_PatientId",
                table: "QueueRooms",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueRooms_Patients_PatientId",
                table: "QueueRooms",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
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
