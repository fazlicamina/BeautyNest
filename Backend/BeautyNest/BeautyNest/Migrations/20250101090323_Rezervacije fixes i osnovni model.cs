using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class Rezervacijefixesiosnovnimodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Rezervacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalonId = table.Column<int>(type: "int", nullable: false),
                    DatumRezervacije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VrijemePocetka = table.Column<TimeSpan>(type: "time", nullable: false),
                    VrijemeZavrsetka = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Saloni_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Saloni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UslugeRezervacije",
                columns: table => new
                {
                    UslugaId = table.Column<int>(type: "int", nullable: false),
                    RezervacijaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UslugeRezervacije", x => new { x.UslugaId, x.RezervacijaId });
                    table.ForeignKey(
                        name: "FK_UslugeRezervacije_Rezervacije_RezervacijaId",
                        column: x => x.RezervacijaId,
                        principalTable: "Rezervacije",
                        principalColumn: "Id",
                         onDelete: ReferentialAction.NoAction);

                    table.ForeignKey(
                        name: "FK_UslugeRezervacije_Usluge_UslugaId",
                        column: x => x.UslugaId,
                        principalTable: "Usluge",
                        principalColumn: "Id",
                         onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_SalonId",
                table: "Rezervacije",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_UslugeRezervacije_RezervacijaId",
                table: "UslugeRezervacije",
                column: "RezervacijaId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {;

            migrationBuilder.DropTable(
                name: "UslugeRezervacije");

            migrationBuilder.DropTable(
                name: "Rezervacije");

        }
    }
}
