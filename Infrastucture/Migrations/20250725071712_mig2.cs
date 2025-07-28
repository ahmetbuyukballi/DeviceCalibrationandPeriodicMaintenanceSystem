using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notificationLogs_meintenancePlan_MeintenancePlanId",
                table: "notificationLogs");

            migrationBuilder.DropIndex(
                name: "IX_notificationLogs_MeintenancePlanId",
                table: "notificationLogs");

            migrationBuilder.DropColumn(
                name: "MeintenancePlanId",
                table: "notificationLogs");

            migrationBuilder.DropColumn(
                name: "lastMaintenceDay",
                table: "meintenancePlan");

            migrationBuilder.DropColumn(
                name: "startMeintenceDay",
                table: "meintenancePlan");

            migrationBuilder.RenameColumn(
                name: "MeintenanceId",
                table: "notificationLogs",
                newName: "MeintenanceRecordId");

            migrationBuilder.AddColumn<DateTime>(
                name: "lastMaintenceDay",
                table: "meintenanceRecord",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "startMeintenceDay",
                table: "meintenanceRecord",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_notificationLogs_MeintenanceRecordId",
                table: "notificationLogs",
                column: "MeintenanceRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_notificationLogs_meintenanceRecord_MeintenanceRecordId",
                table: "notificationLogs",
                column: "MeintenanceRecordId",
                principalTable: "meintenanceRecord",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notificationLogs_meintenanceRecord_MeintenanceRecordId",
                table: "notificationLogs");

            migrationBuilder.DropIndex(
                name: "IX_notificationLogs_MeintenanceRecordId",
                table: "notificationLogs");

            migrationBuilder.DropColumn(
                name: "lastMaintenceDay",
                table: "meintenanceRecord");

            migrationBuilder.DropColumn(
                name: "startMeintenceDay",
                table: "meintenanceRecord");

            migrationBuilder.RenameColumn(
                name: "MeintenanceRecordId",
                table: "notificationLogs",
                newName: "MeintenanceId");

            migrationBuilder.AddColumn<Guid>(
                name: "MeintenancePlanId",
                table: "notificationLogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "lastMaintenceDay",
                table: "meintenancePlan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "startMeintenceDay",
                table: "meintenancePlan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_notificationLogs_MeintenancePlanId",
                table: "notificationLogs",
                column: "MeintenancePlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_notificationLogs_meintenancePlan_MeintenancePlanId",
                table: "notificationLogs",
                column: "MeintenancePlanId",
                principalTable: "meintenancePlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
