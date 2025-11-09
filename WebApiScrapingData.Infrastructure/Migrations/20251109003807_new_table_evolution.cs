using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_table_evolution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EvolutionChainId",
                table: "Pokemons",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EvolvesFromId",
                table: "Pokemons",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EvolutionChain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Evolutions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvolutionChain", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon_EvolveTo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonId = table.Column<long>(type: "bigint", nullable: false),
                    EvolveToId = table.Column<long>(type: "bigint", nullable: false),
                    WhenEvolutionFR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenEvolutionEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenEvolutionES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenEvolutionIT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenEvolutionDE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenEvolutionRU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenEvolutionCO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenEvolutionCN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenEvolutionJP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon_EvolveTo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemon_EvolveTo_Pokemons_EvolveToId",
                        column: x => x.EvolveToId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemon_EvolveTo_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_EvolutionChainId",
                table: "Pokemons",
                column: "EvolutionChainId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_EvolvesFromId",
                table: "Pokemons",
                column: "EvolvesFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_EvolveTo_EvolveToId",
                table: "Pokemon_EvolveTo",
                column: "EvolveToId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_EvolveTo_PokemonId",
                table: "Pokemon_EvolveTo",
                column: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_EvolutionChain_EvolutionChainId",
                table: "Pokemons",
                column: "EvolutionChainId",
                principalTable: "EvolutionChain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Pokemons_EvolvesFromId",
                table: "Pokemons",
                column: "EvolvesFromId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_EvolutionChain_EvolutionChainId",
                table: "Pokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Pokemons_EvolvesFromId",
                table: "Pokemons");

            migrationBuilder.DropTable(
                name: "EvolutionChain");

            migrationBuilder.DropTable(
                name: "Pokemon_EvolveTo");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_EvolutionChainId",
                table: "Pokemons");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_EvolvesFromId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "EvolutionChainId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "EvolvesFromId",
                table: "Pokemons");
        }
    }
}
