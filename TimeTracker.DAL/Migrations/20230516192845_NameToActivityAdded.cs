using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTracker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NameToActivityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAmounts_Users_UserId",
                table: "ProjectAmounts");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Activities",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAmounts_Users_UserId",
                table: "ProjectAmounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAmounts_Users_UserId",
                table: "ProjectAmounts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Activities");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAmounts_Users_UserId",
                table: "ProjectAmounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
