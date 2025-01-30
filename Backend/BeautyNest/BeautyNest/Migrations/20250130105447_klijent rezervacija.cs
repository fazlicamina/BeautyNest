using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class klijentrezervacija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KlijentId",
                table: "Rezervacije",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KlijentId",
                table: "Rezervacije");
        }
    }
}
