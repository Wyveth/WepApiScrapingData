using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class NewColumnPokemonAttack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BasicHappiness",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CaptureRate",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EggMoves",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specie",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CTCS",
                table: "Pokemon_Attaque",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Game",
                table: "Pokemon_Attaque",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Pokemon_Attaque",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeLearn",
                table: "Pokemon_Attaque",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PP",
                table: "Attaques",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "Attaques",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Precision",
                table: "Attaques",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicHappiness",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "CaptureRate",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "EggMoves",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "Specie",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "CTCS",
                table: "Pokemon_Attaque");

            migrationBuilder.DropColumn(
                name: "Game",
                table: "Pokemon_Attaque");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Pokemon_Attaque");

            migrationBuilder.DropColumn(
                name: "TypeLearn",
                table: "Pokemon_Attaque");

            migrationBuilder.DropColumn(
                name: "PP",
                table: "Attaques");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "Attaques");

            migrationBuilder.DropColumn(
                name: "Precision",
                table: "Attaques");
        }
    }
}
