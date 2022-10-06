using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class AddColumnData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "DataAutoHome",
                table: "TypesPok",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DataFondGo",
                table: "TypesPok",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DataIconHome",
                table: "TypesPok",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DataMiniGo",
                table: "TypesPok",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DataMiniHome",
                table: "TypesPok",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DataImg",
                table: "Pokemons",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DataSprite",
                table: "Pokemons",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAutoHome",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "DataFondGo",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "DataIconHome",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "DataMiniGo",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "DataMiniHome",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "DataImg",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "DataSprite",
                table: "Pokemons");
        }
    }
}
