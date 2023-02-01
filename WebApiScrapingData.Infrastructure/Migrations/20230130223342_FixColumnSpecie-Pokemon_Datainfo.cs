using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class FixColumnSpeciePokemon_Datainfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specie",
                table: "Pokemons");

            migrationBuilder.AddColumn<string>(
                name: "Specie",
                table: "DataInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specie",
                table: "DataInfos");

            migrationBuilder.AddColumn<string>(
                name: "Specie",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
