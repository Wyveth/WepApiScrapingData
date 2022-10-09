using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class AddPathFile_DeleteDataFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UserModification",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserCreation",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PathAutoHome",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathFondGo",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathIconHome",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathMiniGo",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathMiniHome_CN",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathMiniHome_CO",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathMiniHome_DE",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathMiniHome_EN",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathMiniHome_ES",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathMiniHome_FR",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathMiniHome_IT",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathMiniHome_JP",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathMiniHome_RU",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModification",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserCreation",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PathImg",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathSprite",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModification",
                table: "Pokemon_Weakness",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserCreation",
                table: "Pokemon_Weakness",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModification",
                table: "Pokemon_TypePok",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserCreation",
                table: "Pokemon_TypePok",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModification",
                table: "DataInfos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserCreation",
                table: "DataInfos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathAutoHome",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PathFondGo",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PathIconHome",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PathMiniGo",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PathMiniHome_CN",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PathMiniHome_CO",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PathMiniHome_DE",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PathMiniHome_EN",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PathMiniHome_ES",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PathMiniHome_FR",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PathMiniHome_IT",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PathMiniHome_JP",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PathMiniHome_RU",
                table: "TypesPok");

            migrationBuilder.DropColumn(
                name: "PathImg",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "PathSprite",
                table: "Pokemons");

            migrationBuilder.AlterColumn<string>(
                name: "UserModification",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserCreation",
                table: "TypesPok",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "UserModification",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserCreation",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "UserModification",
                table: "Pokemon_Weakness",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserCreation",
                table: "Pokemon_Weakness",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModification",
                table: "Pokemon_TypePok",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserCreation",
                table: "Pokemon_TypePok",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModification",
                table: "DataInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserCreation",
                table: "DataInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
