using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class newTableGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Game",
                table: "Pokemon_Attaque");

            migrationBuilder.AddColumn<long>(
                name: "GameId",
                table: "Pokemon_Attaque",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Attaque_GameId",
                table: "Pokemon_Attaque",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemon_Attaque_Games_GameId",
                table: "Pokemon_Attaque",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_Attaque_Games_GameId",
                table: "Pokemon_Attaque");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Pokemon_Attaque_GameId",
                table: "Pokemon_Attaque");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Pokemon_Attaque");

            migrationBuilder.AddColumn<string>(
                name: "Game",
                table: "Pokemon_Attaque",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
