using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLogicService.Migrations
{
    /// <inheritdoc />
    public partial class AddedQuizLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "game_account",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    email_address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "game_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "game_category_game_account",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    game_account_id = table.Column<int>(type: "int", nullable: false),
                    game_category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_category_game_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "played_quiz",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number_of_questions = table.Column<int>(type: "int", nullable: false),
                    number_of_correct_answers = table.Column<int>(type: "int", nullable: false),
                    number_of_incorrect_answers = table.Column<int>(type: "int", nullable: false),
                    category_id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_played_quiz", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "played_quiz_game_account",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    game_account_id = table.Column<int>(type: "int", nullable: false),
                    played_quiz_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_played_quiz_game_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "game_category_tag",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    game_category_id = table.Column<int>(type: "int", nullable: false),
                    game_category_tag_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_category_tag", x => x.id);
                    table.ForeignKey(
                        name: "FK_game_category_tag_game_category_game_category_id",
                        column: x => x.game_category_id,
                        principalTable: "game_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "game_options",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_difficulty_id = table.Column<int>(type: "int", nullable: false),
                    question_category_id = table.Column<int>(type: "int", nullable: false),
                    question_amount = table.Column<int>(type: "int", nullable: false),
                    question_difficulty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_game_options_game_category_question_category_id",
                        column: x => x.question_category_id,
                        principalTable: "game_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_account_username",
                table: "game_account",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_game_category_tag_game_category_id",
                table: "game_category_tag",
                column: "game_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_options_question_category_id",
                table: "game_options",
                column: "question_category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game_account");

            migrationBuilder.DropTable(
                name: "game_category_game_account");

            migrationBuilder.DropTable(
                name: "game_category_tag");

            migrationBuilder.DropTable(
                name: "game_options");

            migrationBuilder.DropTable(
                name: "played_quiz");

            migrationBuilder.DropTable(
                name: "played_quiz_game_account");

            migrationBuilder.DropTable(
                name: "game_category");
        }
    }
}
