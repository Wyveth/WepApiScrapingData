using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class AddTableQuizz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_QuestionAnswers_QuestionAnswerId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Attaques_TypeAttaques_typeAttaqueId",
                table: "Attaques");

            migrationBuilder.DropForeignKey(
                name: "FK_Attaques_TypesPok_typePokId",
                table: "Attaques");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzs_QuizzId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTypes_Difficulties_DifficultyIDId",
                table: "QuestionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Questions_QuizzId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionAnswerId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuizzId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionAnswerId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "ImgNormal",
                table: "QuizzDifficulties",
                newName: "Normal");

            migrationBuilder.RenameColumn(
                name: "ImgHard",
                table: "QuizzDifficulties",
                newName: "Hard");

            migrationBuilder.RenameColumn(
                name: "ImgEasy",
                table: "QuizzDifficulties",
                newName: "Easy");

            migrationBuilder.RenameColumn(
                name: "DifficultyIDId",
                table: "QuestionTypes",
                newName: "DifficultyId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionTypes_DifficultyIDId",
                table: "QuestionTypes",
                newName: "IX_QuestionTypes_DifficultyId");

            migrationBuilder.RenameColumn(
                name: "typePokId",
                table: "Attaques",
                newName: "TypePokId");

            migrationBuilder.RenameColumn(
                name: "typeAttaqueId",
                table: "Attaques",
                newName: "TypeAttaqueId");

            migrationBuilder.RenameIndex(
                name: "IX_Attaques_typePokId",
                table: "Attaques",
                newName: "IX_Attaques_TypePokId");

            migrationBuilder.RenameIndex(
                name: "IX_Attaques_typeAttaqueId",
                table: "Attaques",
                newName: "IX_Attaques_TypeAttaqueId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Difficulties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Question_Answer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    AnswerId = table.Column<long>(type: "bigint", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Answer_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Answer_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswer_Answer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionAnswerId = table.Column<long>(type: "bigint", nullable: false),
                    AnswerId = table.Column<long>(type: "bigint", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswer_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswer_Answer_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionAnswer_Answer_QuestionAnswers_QuestionAnswerId",
                        column: x => x.QuestionAnswerId,
                        principalTable: "QuestionAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quizz_Question",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizzId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizz_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizz_Question_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quizz_Question_Quizzs_QuizzId",
                        column: x => x.QuizzId,
                        principalTable: "Quizzs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_Answer_AnswerId",
                table: "Question_Answer",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Answer_QuestionId",
                table: "Question_Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswer_Answer_AnswerId",
                table: "QuestionAnswer_Answer",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswer_Answer_QuestionAnswerId",
                table: "QuestionAnswer_Answer",
                column: "QuestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizz_Question_QuestionId",
                table: "Quizz_Question",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizz_Question_QuizzId",
                table: "Quizz_Question",
                column: "QuizzId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attaques_TypeAttaques_TypeAttaqueId",
                table: "Attaques",
                column: "TypeAttaqueId",
                principalTable: "TypeAttaques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attaques_TypesPok_TypePokId",
                table: "Attaques",
                column: "TypePokId",
                principalTable: "TypesPok",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTypes_Difficulties_DifficultyId",
                table: "QuestionTypes",
                column: "DifficultyId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attaques_TypeAttaques_TypeAttaqueId",
                table: "Attaques");

            migrationBuilder.DropForeignKey(
                name: "FK_Attaques_TypesPok_TypePokId",
                table: "Attaques");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTypes_Difficulties_DifficultyId",
                table: "QuestionTypes");

            migrationBuilder.DropTable(
                name: "Question_Answer");

            migrationBuilder.DropTable(
                name: "QuestionAnswer_Answer");

            migrationBuilder.DropTable(
                name: "Quizz_Question");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Difficulties");

            migrationBuilder.RenameColumn(
                name: "Normal",
                table: "QuizzDifficulties",
                newName: "ImgNormal");

            migrationBuilder.RenameColumn(
                name: "Hard",
                table: "QuizzDifficulties",
                newName: "ImgHard");

            migrationBuilder.RenameColumn(
                name: "Easy",
                table: "QuizzDifficulties",
                newName: "ImgEasy");

            migrationBuilder.RenameColumn(
                name: "DifficultyId",
                table: "QuestionTypes",
                newName: "DifficultyIDId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionTypes_DifficultyId",
                table: "QuestionTypes",
                newName: "IX_QuestionTypes_DifficultyIDId");

            migrationBuilder.RenameColumn(
                name: "TypePokId",
                table: "Attaques",
                newName: "typePokId");

            migrationBuilder.RenameColumn(
                name: "TypeAttaqueId",
                table: "Attaques",
                newName: "typeAttaqueId");

            migrationBuilder.RenameIndex(
                name: "IX_Attaques_TypePokId",
                table: "Attaques",
                newName: "IX_Attaques_typePokId");

            migrationBuilder.RenameIndex(
                name: "IX_Attaques_TypeAttaqueId",
                table: "Attaques",
                newName: "IX_Attaques_typeAttaqueId");

            migrationBuilder.AddColumn<long>(
                name: "QuizzId",
                table: "Questions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QuestionAnswerId",
                table: "Answers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QuestionId",
                table: "Answers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizzId",
                table: "Questions",
                column: "QuizzId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionAnswerId",
                table: "Answers",
                column: "QuestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_QuestionAnswers_QuestionAnswerId",
                table: "Answers",
                column: "QuestionAnswerId",
                principalTable: "QuestionAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attaques_TypeAttaques_typeAttaqueId",
                table: "Attaques",
                column: "typeAttaqueId",
                principalTable: "TypeAttaques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attaques_TypesPok_typePokId",
                table: "Attaques",
                column: "typePokId",
                principalTable: "TypesPok",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzs_QuizzId",
                table: "Questions",
                column: "QuizzId",
                principalTable: "Quizzs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTypes_Difficulties_DifficultyIDId",
                table: "QuestionTypes",
                column: "DifficultyIDId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
