using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class ManyToManyRelationshipPokemonTypePok : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Pokemon_TypePok",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPokemon = table.Column<long>(type: "bigint", nullable: false),
                    IdTypePok = table.Column<long>(type: "bigint", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon_TypePok", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemon_TypePok_Pokemons_IdPokemon",
                        column: x => x.IdPokemon,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemon_TypePok_TypesPok_IdTypePok",
                        column: x => x.IdTypePok,
                        principalTable: "TypesPok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_TypePok_IdPokemon",
                table: "Pokemon_TypePok",
                column: "IdPokemon");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_TypePok_IdTypePok",
                table: "Pokemon_TypePok",
                column: "IdTypePok");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pokemon_TypePok");

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
    }
}
