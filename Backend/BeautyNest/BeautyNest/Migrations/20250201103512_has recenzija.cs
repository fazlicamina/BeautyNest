using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class hasrecenzija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasRecenzija",
                table: "Rezervacije",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasRecenzija",
                table: "Rezervacije");
        }
    }
}
