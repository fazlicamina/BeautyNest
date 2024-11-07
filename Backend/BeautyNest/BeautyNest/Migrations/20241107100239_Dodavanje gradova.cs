using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class Dodavanjegradova : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GradId",
                table: "Saloni",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Saloni_GradId",
                table: "Saloni",
                column: "GradId");

            migrationBuilder.AddForeignKey(
                name: "FK_Saloni_Gradovi_GradId",
                table: "Saloni",
                column: "GradId",
                principalTable: "Gradovi",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saloni_Gradovi_GradId",
                table: "Saloni");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropIndex(
                name: "IX_Saloni_GradId",
                table: "Saloni");

            migrationBuilder.DropColumn(
                name: "GradId",
                table: "Saloni");
        }
    }
}
