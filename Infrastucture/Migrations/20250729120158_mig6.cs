using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notificationLogs_devices_devicesId",
                table: "notificationLogs");

            migrationBuilder.DropColumn(
                name: "deviceId",
                table: "notificationLogs");

            migrationBuilder.AlterColumn<Guid>(
                name: "devicesId",
                table: "notificationLogs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_notificationLogs_devices_devicesId",
                table: "notificationLogs",
                column: "devicesId",
                principalTable: "devices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notificationLogs_devices_devicesId",
                table: "notificationLogs");

            migrationBuilder.AlterColumn<Guid>(
                name: "devicesId",
                table: "notificationLogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "deviceId",
                table: "notificationLogs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_notificationLogs_devices_devicesId",
                table: "notificationLogs",
                column: "devicesId",
                principalTable: "devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
