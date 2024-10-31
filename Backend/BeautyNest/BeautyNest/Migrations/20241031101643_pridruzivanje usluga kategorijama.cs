using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class pridruzivanjeuslugakategorijama : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KategorijaUslugeId",
                table: "Usluge",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usluge_KategorijaUslugeId",
                table: "Usluge",
                column: "KategorijaUslugeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usluge_KategorijeUsluga_KategorijaUslugeId",
                table: "Usluge",
                column: "KategorijaUslugeId",
                principalTable: "KategorijeUsluga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usluge_KategorijeUsluga_KategorijaUslugeId",
                table: "Usluge");

            migrationBuilder.DropIndex(
                name: "IX_Usluge_KategorijaUslugeId",
                table: "Usluge");

            migrationBuilder.DropColumn(
                name: "KategorijaUslugeId",
                table: "Usluge");
        }
    }
}
