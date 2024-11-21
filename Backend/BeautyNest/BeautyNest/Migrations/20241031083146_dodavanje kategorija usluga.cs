using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class dodavanjekategorijausluga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KategorijeUsluga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategorijeUsluga", x => x.Id);
                });

            migrationBuilder.Sql(@"
                INSERT INTO KategorijeUsluga (Naziv) 
                VALUES 
                ('Šišanje'),
                ('Feniranje'),
                ('Farbanje'),
                ('Pramenovi'),
                ('Manikir'),
                ('Pedikir'),
                ('Nadogranja noktiju'),
                ('Frizura'),
                ('Manikura'),
                ('Kozmetički tretmani'),
                ('Tretman lica'),
                ('Masaža'),
                ('Masaža')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KategorijeUsluga");
        }
    }
}
