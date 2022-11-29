using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class AddAttaque_TypeAttaque_TalentHidden_SoundUrlPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathSound",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlSound",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Pokemon_Talent",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TypeAttaques",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAttaques", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attaques",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    typeAttaqueId = table.Column<long>(type: "bigint", nullable: true),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attaques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attaques_TypeAttaques_typeAttaqueId",
                        column: x => x.typeAttaqueId,
                        principalTable: "TypeAttaques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon_Attaque",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonId = table.Column<long>(type: "bigint", nullable: false),
                    AttaqueId = table.Column<long>(type: "bigint", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon_Attaque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemon_Attaque_Attaques_AttaqueId",
                        column: x => x.AttaqueId,
                        principalTable: "Attaques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemon_Attaque_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attaques_typeAttaqueId",
                table: "Attaques",
                column: "typeAttaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Attaque_AttaqueId",
                table: "Pokemon_Attaque",
                column: "AttaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Attaque_PokemonId",
                table: "Pokemon_Attaque",
                column: "PokemonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pokemon_Attaque");

            migrationBuilder.DropTable(
                name: "Attaques");

            migrationBuilder.DropTable(
                name: "TypeAttaques");

            migrationBuilder.DropColumn(
                name: "PathSound",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "UrlSound",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Pokemon_Talent");
        }
    }
}
