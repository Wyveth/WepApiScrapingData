using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class AddTalents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTypes_QuizzDifficulties_DifficultyIDId",
                table: "QuestionTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzs_AspNetUsers_identityUserId",
                table: "Quizzs");

            migrationBuilder.RenameColumn(
                name: "identityUserId",
                table: "Quizzs",
                newName: "IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzs_identityUserId",
                table: "Quizzs",
                newName: "IX_Quizzs_IdentityUserId");

            migrationBuilder.CreateTable(
                name: "Talents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_FR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_EN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_ES = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_ES = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_IT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_IT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_DE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_DE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_RU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_RU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_CO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_CO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_CN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_CN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_JP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_JP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon_Talent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonId = table.Column<long>(type: "bigint", nullable: false),
                    TalentId = table.Column<long>(type: "bigint", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon_Talent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemon_Talent_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemon_Talent_Talents_TalentId",
                        column: x => x.TalentId,
                        principalTable: "Talents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Talent_PokemonId",
                table: "Pokemon_Talent",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Talent_TalentId",
                table: "Pokemon_Talent",
                column: "TalentId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTypes_Difficulties_DifficultyIDId",
                table: "QuestionTypes",
                column: "DifficultyIDId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzs_AspNetUsers_IdentityUserId",
                table: "Quizzs",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTypes_Difficulties_DifficultyIDId",
                table: "QuestionTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzs_AspNetUsers_IdentityUserId",
                table: "Quizzs");

            migrationBuilder.DropTable(
                name: "Pokemon_Talent");

            migrationBuilder.DropTable(
                name: "Talents");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "Quizzs",
                newName: "identityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzs_IdentityUserId",
                table: "Quizzs",
                newName: "IX_Quizzs_identityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTypes_QuizzDifficulties_DifficultyIDId",
                table: "QuestionTypes",
                column: "DifficultyIDId",
                principalTable: "QuizzDifficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzs_AspNetUsers_identityUserId",
                table: "Quizzs",
                column: "identityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
