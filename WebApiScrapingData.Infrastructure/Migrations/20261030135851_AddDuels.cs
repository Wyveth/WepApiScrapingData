using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDuels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FriendCode",
                table: "UserProfiles",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AnsweredAt",
                table: "Questions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShownAt",
                table: "Questions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Duels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DifficultyCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Generations = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    Player1Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Player2Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Quizz1Id = table.Column<long>(type: "bigint", nullable: true),
                    Quizz2Id = table.Column<long>(type: "bigint", nullable: true),
                    Score1 = table.Column<int>(type: "int", nullable: false),
                    Score2 = table.Column<int>(type: "int", nullable: false),
                    WinnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForfeitedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentOrder = table.Column<int>(type: "int", nullable: false),
                    QuestionStartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QuestionSeconds = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Duels_AspNetUsers_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Duels_AspNetUsers_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Duels_Quizzs_Quizz1Id",
                        column: x => x.Quizz1Id,
                        principalTable: "Quizzs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Duels_Quizzs_Quizz2Id",
                        column: x => x.Quizz2Id,
                        principalTable: "Quizzs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_FriendCode",
                table: "UserProfiles",
                column: "FriendCode",
                unique: true,
                filter: "[FriendCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Duels_Guid",
                table: "Duels",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Duels_Player1Id",
                table: "Duels",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Duels_Player2Id",
                table: "Duels",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Duels_Quizz1Id",
                table: "Duels",
                column: "Quizz1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Duels_Quizz2Id",
                table: "Duels",
                column: "Quizz2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Duels_Status_Mode_DifficultyCode",
                table: "Duels",
                columns: new[] { "Status", "Mode", "DifficultyCode" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Duels");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_FriendCode",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "FriendCode",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "AnsweredAt",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ShownAt",
                table: "Questions");
        }
    }
}
