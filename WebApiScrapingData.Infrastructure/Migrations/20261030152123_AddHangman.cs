using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHangman : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HangmanGames",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Generations = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Player1Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Player2Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CurrentTurnUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lives = table.Column<int>(type: "int", nullable: false),
                    MaxLives = table.Column<int>(type: "int", nullable: false),
                    Found = table.Column<int>(type: "int", nullable: false),
                    CurrentPokemonId = table.Column<long>(type: "bigint", nullable: true),
                    GuessedLetters = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    WrongCount = table.Column<int>(type: "int", nullable: false),
                    UsedPokemonIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastPokemonId = table.Column<long>(type: "bigint", nullable: true),
                    LastFound = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangmanGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HangmanGames_AspNetUsers_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HangmanGames_AspNetUsers_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HangmanGames_Guid",
                table: "HangmanGames",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HangmanGames_Player1Id",
                table: "HangmanGames",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_HangmanGames_Player2Id",
                table: "HangmanGames",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_HangmanGames_Status_Player2Id",
                table: "HangmanGames",
                columns: new[] { "Status", "Player2Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HangmanGames");
        }
    }
}
