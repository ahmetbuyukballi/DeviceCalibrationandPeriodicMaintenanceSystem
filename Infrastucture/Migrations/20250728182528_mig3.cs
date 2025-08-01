using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_meintenanceRecord_AspNetUsers_appUserId",
                table: "meintenanceRecord");

            migrationBuilder.DropIndex(
                name: "IX_meintenanceRecord_appUserId",
                table: "meintenanceRecord");

            migrationBuilder.DropColumn(
                name: "appUserId",
                table: "meintenanceRecord");

            migrationBuilder.CreateIndex(
                name: "IX_meintenanceRecord_userId",
                table: "meintenanceRecord",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_meintenanceRecord_AspNetUsers_userId",
                table: "meintenanceRecord",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_meintenanceRecord_AspNetUsers_userId",
                table: "meintenanceRecord");

            migrationBuilder.DropIndex(
                name: "IX_meintenanceRecord_userId",
                table: "meintenanceRecord");

            migrationBuilder.AddColumn<Guid>(
                name: "appUserId",
                table: "meintenanceRecord",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_meintenanceRecord_appUserId",
                table: "meintenanceRecord",
                column: "appUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_meintenanceRecord_AspNetUsers_appUserId",
                table: "meintenanceRecord",
                column: "appUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
