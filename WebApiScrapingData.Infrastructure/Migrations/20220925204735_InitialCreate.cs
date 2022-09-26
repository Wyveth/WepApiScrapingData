using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionVx = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionVy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Talent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionTalent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Types = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weakness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Evolutions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenEvolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesPok",
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
                    Name_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlMiniGo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlFondGo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlMiniHome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlIconHome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlAutoHome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfoColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesPok", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_FR = table.Column<long>(type: "bigint", nullable: false),
                    Id_EN = table.Column<long>(type: "bigint", nullable: false),
                    Id_ES = table.Column<long>(type: "bigint", nullable: false),
                    Id_IT = table.Column<long>(type: "bigint", nullable: false),
                    Id_DE = table.Column<long>(type: "bigint", nullable: false),
                    Id_RU = table.Column<long>(type: "bigint", nullable: false),
                    Id_CO = table.Column<long>(type: "bigint", nullable: false),
                    Id_CN = table.Column<long>(type: "bigint", nullable: false),
                    Id_JP = table.Column<long>(type: "bigint", nullable: false),
                    TypeEvolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatPv = table.Column<int>(type: "int", nullable: false),
                    StatAttaque = table.Column<int>(type: "int", nullable: false),
                    StatDefense = table.Column<int>(type: "int", nullable: false),
                    StatAttaqueSpe = table.Column<int>(type: "int", nullable: false),
                    StatDefenseSpe = table.Column<int>(type: "int", nullable: false),
                    StatVitesse = table.Column<int>(type: "int", nullable: false),
                    StatTotal = table.Column<int>(type: "int", nullable: false),
                    Generation = table.Column<int>(type: "int", nullable: false),
                    UrlImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlSprite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_CN",
                        column: x => x.Id_CN,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_CO",
                        column: x => x.Id_CO,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_DE",
                        column: x => x.Id_DE,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_EN",
                        column: x => x.Id_EN,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_ES",
                        column: x => x.Id_ES,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_FR",
                        column: x => x.Id_FR,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_IT",
                        column: x => x.Id_IT,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_JP",
                        column: x => x.Id_JP,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_RU",
                        column: x => x.Id_RU,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_CN",
                table: "Pokemons",
                column: "Id_CN");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_CO",
                table: "Pokemons",
                column: "Id_CO");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_DE",
                table: "Pokemons",
                column: "Id_DE");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_EN",
                table: "Pokemons",
                column: "Id_EN");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_ES",
                table: "Pokemons",
                column: "Id_ES");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_FR",
                table: "Pokemons",
                column: "Id_FR");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_IT",
                table: "Pokemons",
                column: "Id_IT");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_JP",
                table: "Pokemons",
                column: "Id_JP");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_RU",
                table: "Pokemons",
                column: "Id_RU");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.DropTable(
                name: "TypesPok");

            migrationBuilder.DropTable(
                name: "DataInfos");
        }
    }
}
