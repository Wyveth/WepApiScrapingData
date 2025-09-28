using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class UpdateLibelleQuestionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Libelle",
                table: "QuestionTypes",
                newName: "Libelle_RU");

            migrationBuilder.AddColumn<string>(
                name: "Libelle_CN",
                table: "QuestionTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Libelle_CO",
                table: "QuestionTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Libelle_DE",
                table: "QuestionTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Libelle_EN",
                table: "QuestionTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Libelle_ES",
                table: "QuestionTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Libelle_FR",
                table: "QuestionTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Libelle_IT",
                table: "QuestionTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Libelle_JP",
                table: "QuestionTypes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Libelle_CN",
                table: "QuestionTypes");

            migrationBuilder.DropColumn(
                name: "Libelle_CO",
                table: "QuestionTypes");

            migrationBuilder.DropColumn(
                name: "Libelle_DE",
                table: "QuestionTypes");

            migrationBuilder.DropColumn(
                name: "Libelle_EN",
                table: "QuestionTypes");

            migrationBuilder.DropColumn(
                name: "Libelle_ES",
                table: "QuestionTypes");

            migrationBuilder.DropColumn(
                name: "Libelle_FR",
                table: "QuestionTypes");

            migrationBuilder.DropColumn(
                name: "Libelle_IT",
                table: "QuestionTypes");

            migrationBuilder.DropColumn(
                name: "Libelle_JP",
                table: "QuestionTypes");

            migrationBuilder.RenameColumn(
                name: "Libelle_RU",
                table: "QuestionTypes",
                newName: "Libelle");
        }
    }
}
