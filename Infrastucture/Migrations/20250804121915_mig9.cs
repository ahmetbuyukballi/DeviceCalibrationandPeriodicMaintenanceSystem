using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedback_devices_devicesId",
                table: "feedback");

            migrationBuilder.DropIndex(
                name: "IX_feedback_devicesId",
                table: "feedback");

            migrationBuilder.DropColumn(
                name: "devicesId",
                table: "feedback");

            migrationBuilder.CreateIndex(
                name: "IX_feedback_DeviceId",
                table: "feedback",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_feedback_devices_DeviceId",
                table: "feedback",
                column: "DeviceId",
                principalTable: "devices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedback_devices_DeviceId",
                table: "feedback");

            migrationBuilder.DropIndex(
                name: "IX_feedback_DeviceId",
                table: "feedback");

            migrationBuilder.AddColumn<Guid>(
                name: "devicesId",
                table: "feedback",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_feedback_devicesId",
                table: "feedback",
                column: "devicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_feedback_devices_devicesId",
                table: "feedback",
                column: "devicesId",
                principalTable: "devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
