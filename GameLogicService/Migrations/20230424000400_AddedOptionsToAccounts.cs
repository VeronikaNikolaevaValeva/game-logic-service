using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLogicService.Migrations
{
    /// <inheritdoc />
    public partial class AddedOptionsToAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "game_options_id",
                table: "game_category_game_account",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "game_options_id",
                table: "game_category_game_account");
        }
    }
}
