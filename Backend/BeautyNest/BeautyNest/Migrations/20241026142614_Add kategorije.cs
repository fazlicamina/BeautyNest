using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class Addkategorije : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KategorijaSalon",
                columns: table => new
                {
                    KategorijeId = table.Column<int>(type: "int", nullable: false),
                    SaloniId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategorijaSalon", x => new { x.KategorijeId, x.SaloniId });
                    table.ForeignKey(
                        name: "FK_KategorijaSalon_Kategorije_KategorijeId",
                        column: x => x.KategorijeId,
                        principalTable: "Kategorije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KategorijaSalon_Saloni_SaloniId",
                        column: x => x.SaloniId,
                        principalTable: "Saloni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KategorijaSalon_SaloniId",
                table: "KategorijaSalon",
                column: "SaloniId");

            migrationBuilder.Sql(@"
                INSERT INTO KategorijaSalon (SaloniId, KategorijeId) VALUES
                (1, 1),
                (1, 2),
                (2, 3),
                (2, 4),
                (3, 1),
                (3, 2),
                (3, 3),
                (4, 4),
                (4, 2);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KategorijaSalon");
        }
    }
}
