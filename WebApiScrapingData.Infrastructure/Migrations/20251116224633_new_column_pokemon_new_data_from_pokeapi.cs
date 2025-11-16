using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_column_pokemon_new_data_from_pokeapi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasGenderDifferences",
                table: "Pokemons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBaby",
                table: "Pokemons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLegendary",
                table: "Pokemons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMythical",
                table: "Pokemons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "HasGenderDifferences",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "IsBaby",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "IsLegendary",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "IsMythical",
                table: "Pokemons");
        }
    }
}
