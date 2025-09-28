using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class add_column_img_sprite_sound_normal_shiny : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathImgNormal",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathImgShiny",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathSoundCurrent",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathSoundLegacy",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathSpriteNormal",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathSpriteShiny",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathImgNormal",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "PathImgShiny",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "PathSoundCurrent",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "PathSoundLegacy",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "PathSpriteNormal",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "PathSpriteShiny",
                table: "Pokemons");
        }
    }
}
