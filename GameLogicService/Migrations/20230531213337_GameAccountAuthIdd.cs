using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLogicService.Migrations
{
    /// <inheritdoc />
    public partial class GameAccountAuthIdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "auth_id",
                table: "game_account_auth_id",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "auth_id",
                table: "game_account_auth_id",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
