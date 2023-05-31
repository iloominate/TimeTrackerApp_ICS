using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTracker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Fixrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAmounts_Users_UserId",
                table: "ProjectAmounts");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectEntityId",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ProjectEntityId",
                table: "Activities",
                column: "ProjectEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Projects_ProjectEntityId",
                table: "Activities",
                column: "ProjectEntityId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAmounts_Users_UserId",
                table: "ProjectAmounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Projects_ProjectEntityId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAmounts_Users_UserId",
                table: "ProjectAmounts");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ProjectEntityId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ProjectEntityId",
                table: "Activities");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAmounts_Users_UserId",
                table: "ProjectAmounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
