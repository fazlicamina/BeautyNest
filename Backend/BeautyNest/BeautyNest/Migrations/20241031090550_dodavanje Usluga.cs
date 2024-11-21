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
                ('Šišanje duge kose', 30, '00:45:00'),
                ('Šišanje kratke kose', 20, '00:30:00'),
                ('Šišanje pixie cut', 25, '00:40:00'),
                ('Feniranje duge kose', 20, '00:30:00'),
                ('Feniranje kratke kose', 15, '00:20:00'),
                ('Feniranje sa volumenom', 25, '00:35:00'),
                ('Farbanje korijena', 15, '00:50:00'),
                ('Farbanje cijele kose', 20, '01:15:00'),
                ('Farbanje sa preljevom', 18, '01:00:00'),
                ('Pramenovi folijom', 40, '01:10:00'),
                ('Pramenovi češljem', 40, '00:50:00'),
                ('Balayage', 40, '01:30:00'),
                ('Klasični manikir', 10, '00:30:00'),
                ('Francuska manikura', 15, '00:40:00'),
                ('Gel manikura', 25, '00:50:00'),
                ('Klasični pedikir', 20, '00:45:00'),
                ('Spa pedikir', 25, '01:00:00'),
                ('Medicinski pedikir', 30, '01:15:00'),
                ('Akrilna nadogradnja', 20, '01:30:00'),
                ('Gel nadogradnja', 25, '01:15:00'),
                ('Dip powder nadogradnja', 50, '01:40:00'),
                ('Relax masaža', 50, '00:40:00'),
                ('Anticelulit masaža', 80, '00:40:00'),
                ('Antistres masaža', 80, '00:40:00'),
                ('Klasični manikir', 10, '00:30:00'),
                ('Francuska manikura', 15, '00:40:00'),
                ('Gel manikura', 25, '00:50:00'),
                ('Šišanje duge kose', 30, '00:45:00'),
                ('Šišanje kratke kose', 20, '00:30:00'),
                ('Šišanje pixie cut', 25, '00:40:00'),

                ('Enzimski peeling', 50, '00:40:00'),
                ('Masaža lica', 25, '00:20:00'),

                ('Relax masaža', 50, '00:40:00'),
                ('Anticelulit masaža', 25, '00:50:00'),

                ('Masaža lica', 25, '00:20:00'),
                ('Relax masaža', 50, '00:40:00'),
                ('Anticelulit masaža', 25, '00:50:00')

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
