using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTracker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RefactorProjectEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_CreatorIdId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ActivityType",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "CreatorIdId",
                table: "Projects",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CreatorIdId",
                table: "Projects",
                newName: "IX_Projects_CreatorId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Activities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_CreatorId",
                table: "Projects",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_CreatorId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Projects",
                newName: "CreatorIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CreatorId",
                table: "Projects",
                newName: "IX_Projects_CreatorIdId");

            migrationBuilder.AddColumn<string>(
                name: "ActivityType",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_CreatorIdId",
                table: "Projects",
                column: "CreatorIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
