using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class dodavanjekategorijaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.Id);
                });

            migrationBuilder.InsertData(
            table: "Kategorije",
            columns: new[] { "Naziv" },
            values: new object[,]
            {
                { "Frizerske usluge" },
                { "Kozmetički tretmani" },
                { "Manikir i pedikir" },
                { "Spa tretmani" },
                { "Masaže" },
                {"Estetska medicina" },
                {"Trajna šminka" },
                {"Obrve i trepavice" }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kategorije");
        }
    }
}
