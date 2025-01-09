using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class dodavanjeUsluga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usluge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Trajanje = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usluge", x => x.Id);
                });

            migrationBuilder.Sql(@"
                INSERT INTO Usluge (Naziv, Cijena, Trajanje) 
                VALUES 
                ('Šišanje duge kose', 30, '00:30:00'),
                ('Šišanje kratke kose', 20, '00:30:00'),
                ('Šišanje pixie cut', 25, '01:00:00'),
                ('Feniranje duge kose', 20, '00:30:00'),
                ('Feniranje kratke kose', 15, '00:30:00'),
                ('Feniranje sa volumenom', 25, '00:30:00'),
                ('Farbanje korijena', 15, '01:00:00'),
                ('Farbanje cijele kose', 20, '01:30:00'),
                ('Farbanje sa preljevom', 18, '02:00:00'),
                ('Pramenovi folijom', 40, '01:30:00'),
                ('Pramenovi češljem', 40, '01:30:00'),
                ('Balayage', 40, '02:30:00'),
                ('Klasični manikir', 10, '00:30:00'),
                ('Francuska manikura', 15, '00:30:00'),
                ('Gel manikura', 25, '00:30:00'),
                ('Klasični pedikir', 20, '00:30:00'),
                ('Spa pedikir', 25, '01:00:00'),
                ('Medicinski pedikir', 30, '01:00:00'),
                ('Akrilna nadogradnja', 20, '01:00:00'),
                ('Gel nadogradnja', 25, '01:00:00'),
                ('Dip powder nadogradnja', 50, '01:00:00'),
                ('Relax masaža', 50, '00:30:00'),
                ('Anticelulit masaža', 80, '01:00:00'),
                ('Antistres masaža', 80, '00:00:00'),
                ('Klasični manikir', 10, '00:30:00'),
                ('Francuska manikura', 15, '00:30:00'),
                ('Gel manikura', 25, '01:00:00'),
                ('Šišanje duge kose', 30, '00:30:00'),
                ('Šišanje kratke kose', 20, '00:30:00'),
                ('Šišanje pixie cut', 25, '00:30:00'),

                ('Enzimski peeling', 50, '00:30:00'),
                ('Masaža lica', 25, '00:30:00'),

                ('Relax masaža', 50, '01:00:00'),
                ('Anticelulit masaža', 25, '01:00:00'),

                ('Masaža lica', 25, '00:30:00'),
                ('Relax masaža', 50, '01:00:00'),
                ('Anticelulit masaža', 25, '01:00:00')

            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usluge");
        }
    }
}
