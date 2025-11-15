using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class remove_column_new_feature_evolveto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Pokemons_EvolvesFromId",
                table: "Pokemons");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_EvolvesFromId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "EvolvesFromId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "DescriptionTalent",
                table: "DataInfos");

            migrationBuilder.DropColumn(
                name: "Talent",
                table: "DataInfos");

            migrationBuilder.DropColumn(
                name: "Types",
                table: "DataInfos");

            migrationBuilder.DropColumn(
                name: "Weakness",
                table: "DataInfos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EvolvesFromId",
                table: "Pokemons",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionTalent",
                table: "DataInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Talent",
                table: "DataInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Types",
                table: "DataInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weakness",
                table: "DataInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_EvolvesFromId",
                table: "Pokemons",
                column: "EvolvesFromId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Pokemons_EvolvesFromId",
                table: "Pokemons",
                column: "EvolvesFromId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
