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
                nullable: true,
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

            migrationBuilder.Sql(@"
                UPDATE Usluge SET KategorijaUslugeId = 1 WHERE Id IN (1, 2, 3);
                UPDATE Usluge SET KategorijaUslugeId = 2 WHERE Id IN (4, 5, 6);
                UPDATE Usluge SET KategorijaUslugeId = 3 WHERE Id IN (7, 8, 9);
                UPDATE Usluge SET KategorijaUslugeId = 4 WHERE Id IN (10, 11, 12);
                UPDATE Usluge SET KategorijaUslugeId = 5 WHERE Id IN (13, 14, 15);
                UPDATE Usluge SET KategorijaUslugeId = 6 WHERE Id IN (16, 17, 18);
                UPDATE Usluge SET KategorijaUslugeId = 7 WHERE Id IN (19, 20, 21);
                UPDATE Usluge SET KategorijaUslugeId = 9 WHERE Id IN (25, 26, 27);
                UPDATE Usluge SET KategorijaUslugeId = 10 WHERE Id IN (22, 23, 24);

                UPDATE Usluge SET KategorijaUslugeId = 8 WHERE Id IN (28, 29, 30);

                UPDATE Usluge SET KategorijaUslugeId = 11 WHERE Id IN (31, 32); 
                UPDATE Usluge SET KategorijaUslugeId = 12 WHERE Id IN (33, 34);
                UPDATE Usluge SET KategorijaUslugeId = 13 WHERE Id IN (35, 36, 37);
            ");
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
