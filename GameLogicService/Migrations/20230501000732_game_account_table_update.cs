using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLogicService.Migrations
{
    /// <inheritdoc />
    public partial class game_account_table_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_account_username",
                table: "game_account");

            migrationBuilder.AlterColumn<int>(
                name: "category_id",
                table: "played_quiz",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "game_account",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "email_address",
                table: "game_account",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "idx_account_emaild_address",
                table: "game_account",
                column: "email_address",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_account_emaild_address",
                table: "game_account");

            migrationBuilder.AlterColumn<string>(
                name: "category_id",
                table: "played_quiz",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "game_account",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "email_address",
                table: "game_account",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "idx_account_username",
                table: "game_account",
                column: "username",
                unique: true);
        }
    }
}
