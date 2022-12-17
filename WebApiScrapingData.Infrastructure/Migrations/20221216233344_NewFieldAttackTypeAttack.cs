using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldAttackTypeAttack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathImg",
                table: "TypeAttaques",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlImg",
                table: "TypeAttaques",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "typePokId",
                table: "Attaques",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attaques_typePokId",
                table: "Attaques",
                column: "typePokId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attaques_TypesPok_typePokId",
                table: "Attaques",
                column: "typePokId",
                principalTable: "TypesPok",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attaques_TypesPok_typePokId",
                table: "Attaques");

            migrationBuilder.DropIndex(
                name: "IX_Attaques_typePokId",
                table: "Attaques");

            migrationBuilder.DropColumn(
                name: "PathImg",
                table: "TypeAttaques");

            migrationBuilder.DropColumn(
                name: "UrlImg",
                table: "TypeAttaques");

            migrationBuilder.DropColumn(
                name: "typePokId",
                table: "Attaques");
        }
    }
}
