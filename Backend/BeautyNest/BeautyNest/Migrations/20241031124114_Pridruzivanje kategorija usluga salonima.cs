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
                nullable: true,
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

            migrationBuilder.Sql(@"
                UPDATE KategorijeUsluga SET SalonId = 1 WHERE Id IN (1, 2, 3, 4);
                UPDATE KategorijeUsluga SET SalonId = 2 WHERE Id IN (5, 6, 7, 12);
                UPDATE KategorijeUsluga SET SalonId = 3 WHERE Id IN (8, 9, 10);
                UPDATE KategorijeUsluga SET SalonId = 4 WHERE Id IN (11, 13);
            ");
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
