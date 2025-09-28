using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class updatecolumn_pathimg_sprite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PathSprite",
                table: "Pokemons",
                newName: "PathSpriteLegacy");

            migrationBuilder.RenameColumn(
                name: "PathImg",
                table: "Pokemons",
                newName: "PathImgLegacy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PathSpriteLegacy",
                table: "Pokemons",
                newName: "PathSprite");

            migrationBuilder.RenameColumn(
                name: "PathImgLegacy",
                table: "Pokemons",
                newName: "PathImg");
        }
    }
}
