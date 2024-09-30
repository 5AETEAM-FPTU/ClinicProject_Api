using System;
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
                name: "FK_Appointments_OnlinePayments_OnlinePaymentId",
                table: "Appointments"
            );

            migrationBuilder.DropIndex(
                name: "IX_Appointments_OnlinePaymentId",
                table: "Appointments"
            );

            migrationBuilder.DropColumn(name: "OnlinePaymentId", table: "Appointments");

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentId",
                table: "OnlinePayments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
            );

            migrationBuilder.CreateIndex(
                name: "IX_OnlinePayments_AppointmentId",
                table: "OnlinePayments",
                column: "AppointmentId",
                unique: true
            );

            migrationBuilder.AddForeignKey(
                name: "FK_OnlinePayments_Appointments_AppointmentId",
                table: "OnlinePayments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlinePayments_Appointments_AppointmentId",
                table: "OnlinePayments"
            );

            migrationBuilder.DropIndex(
                name: "IX_OnlinePayments_AppointmentId",
                table: "OnlinePayments"
            );

            migrationBuilder.DropColumn(name: "AppointmentId", table: "OnlinePayments");

            migrationBuilder.AddColumn<Guid>(
                name: "OnlinePaymentId",
                table: "Appointments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
            );

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_OnlinePaymentId",
                table: "Appointments",
                column: "OnlinePaymentId",
                unique: true
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_OnlinePayments_OnlinePaymentId",
                table: "Appointments",
                column: "OnlinePaymentId",
                principalTable: "OnlinePayments",
                principalColumn: "Id"
            );
        }
    }
}
