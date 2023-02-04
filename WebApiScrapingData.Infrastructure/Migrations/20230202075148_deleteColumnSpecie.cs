using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    public partial class deleteColumnSpecie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specie",
                table: "DataInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Specie",
                table: "DataInfos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
