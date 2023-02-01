using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class ChangeColumGameAttaqueToPokemon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_Attaque_Games_GameId",
                table: "Pokemon_Attaque");

            migrationBuilder.DropIndex(
                name: "IX_Pokemon_Attaque_GameId",
                table: "Pokemon_Attaque");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Pokemon_Attaque");

            migrationBuilder.AddColumn<long>(
                name: "GameId",
                table: "Pokemons",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_GameId",
                table: "Pokemons",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Games_GameId",
                table: "Pokemons",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Games_GameId",
                table: "Pokemons");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_GameId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Pokemons");

            migrationBuilder.AddColumn<long>(
                name: "GameId",
                table: "Pokemon_Attaque",
                type: "bigint",
                nullable: true);

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
    }
}
