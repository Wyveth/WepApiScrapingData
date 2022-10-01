using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class AddListTypePokInPokemon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PokemonId",
                table: "TypesPok",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypesPok_PokemonId",
                table: "TypesPok",
                column: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypesPok_Pokemons_PokemonId",
                table: "TypesPok",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypesPok_Pokemons_PokemonId",
                table: "TypesPok");

            migrationBuilder.DropIndex(
                name: "IX_TypesPok_PokemonId",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "TypesPok");
        }
    }
}
