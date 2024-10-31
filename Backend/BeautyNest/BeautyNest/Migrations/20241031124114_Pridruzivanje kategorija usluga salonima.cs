using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class Pridruzivanjekategorijauslugasalonima : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "KategorijeUsluga",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_KategorijeUsluga_SalonId",
                table: "KategorijeUsluga",
                column: "SalonId");

            migrationBuilder.AddForeignKey(
                name: "FK_KategorijeUsluga_Saloni_SalonId",
                table: "KategorijeUsluga",
                column: "SalonId",
                principalTable: "Saloni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KategorijeUsluga_Saloni_SalonId",
                table: "KategorijeUsluga");

            migrationBuilder.DropIndex(
                name: "IX_KategorijeUsluga_SalonId",
                table: "KategorijeUsluga");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "KategorijeUsluga");
        }
    }
}
