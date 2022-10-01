using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class ManyToManyRelationshipPokemonWeakness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_TypePok_Pokemons_IdPokemon",
                table: "Pokemon_TypePok");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_TypePok_TypesPok_IdTypePok",
                table: "Pokemon_TypePok");

            migrationBuilder.RenameColumn(
                name: "IdTypePok",
                table: "Pokemon_TypePok",
                newName: "TypePokId");

            migrationBuilder.RenameColumn(
                name: "IdPokemon",
                table: "Pokemon_TypePok",
                newName: "PokemonId");

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_TypePok_IdTypePok",
                table: "Pokemon_TypePok",
                newName: "IX_Pokemon_TypePok_TypePokId");

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_TypePok_IdPokemon",
                table: "Pokemon_TypePok",
                newName: "IX_Pokemon_TypePok_PokemonId");

            migrationBuilder.CreateTable(
                name: "Pokemon_Weakness",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonId = table.Column<long>(type: "bigint", nullable: false),
                    TypePokId = table.Column<long>(type: "bigint", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon_Weakness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemon_Weakness_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemon_Weakness_TypesPok_TypePokId",
                        column: x => x.TypePokId,
                        principalTable: "TypesPok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Weakness_PokemonId",
                table: "Pokemon_Weakness",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Weakness_TypePokId",
                table: "Pokemon_Weakness",
                column: "TypePokId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemon_TypePok_Pokemons_PokemonId",
                table: "Pokemon_TypePok",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemon_TypePok_TypesPok_TypePokId",
                table: "Pokemon_TypePok",
                column: "TypePokId",
                principalTable: "TypesPok",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_TypePok_Pokemons_PokemonId",
                table: "Pokemon_TypePok");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_TypePok_TypesPok_TypePokId",
                table: "Pokemon_TypePok");

            migrationBuilder.DropTable(
                name: "Pokemon_Weakness");

            migrationBuilder.RenameColumn(
                name: "TypePokId",
                table: "Pokemon_TypePok",
                newName: "IdTypePok");

            migrationBuilder.RenameColumn(
                name: "PokemonId",
                table: "Pokemon_TypePok",
                newName: "IdPokemon");

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_TypePok_TypePokId",
                table: "Pokemon_TypePok",
                newName: "IX_Pokemon_TypePok_IdTypePok");

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_TypePok_PokemonId",
                table: "Pokemon_TypePok",
                newName: "IX_Pokemon_TypePok_IdPokemon");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemon_TypePok_Pokemons_IdPokemon",
                table: "Pokemon_TypePok",
                column: "IdPokemon",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemon_TypePok_TypesPok_IdTypePok",
                table: "Pokemon_TypePok",
                column: "IdTypePok",
                principalTable: "TypesPok",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
