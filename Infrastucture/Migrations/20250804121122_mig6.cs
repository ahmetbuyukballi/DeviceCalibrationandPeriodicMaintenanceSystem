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
                name: "FK_feedback_User_AppUsersId",
                table: "feedback");

            migrationBuilder.DropIndex(
                name: "IX_feedback_AppUsersId",
                table: "feedback");

            migrationBuilder.DropColumn(
                name: "AppUsersId",
                table: "feedback");

            migrationBuilder.CreateIndex(
                name: "IX_feedback_UserId",
                table: "feedback",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_feedback_User_UserId",
                table: "feedback",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedback_User_UserId",
                table: "feedback");

            migrationBuilder.DropIndex(
                name: "IX_feedback_UserId",
                table: "feedback");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUsersId",
                table: "feedback",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_feedback_AppUsersId",
                table: "feedback",
                column: "AppUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_feedback_User_AppUsersId",
                table: "feedback",
                column: "AppUsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
