using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class Dodavanjegradova : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GradId",
                table: "Saloni",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Saloni_GradId",
                table: "Saloni",
                column: "GradId");

            migrationBuilder.AddForeignKey(
                name: "FK_Saloni_Gradovi_GradId",
                table: "Saloni",
                column: "GradId",
                principalTable: "Gradovi",
                principalColumn: "Id");

            migrationBuilder.Sql(@"
    INSERT INTO Gradovi (Naziv) VALUES
    ('Sarajevo'),
    ('Banja Luka'),
    ('Mostar'),
    ('Zenica'),
    ('Tuzla'),
    ('Bijeljina'),
    ('Brcko'),
    ('Bihac'),
    ('Gradacac'),
    ('Doboj'),
    ('Livno'),
    ('Jajce'),
    ('Trebinje'),
    ('Kakanj'),
    ('Capljina'),
    ('Zavidovici'),
    ('Cazin'),
    ('Maglaj'),
    ('Novi Travnik'),
    ('Fojnica'),
    ('Bosanska Krupa'),
    ('Vitez'),
    ('Tešanj'),
    ('Bugojno'),
    ('Visoko'),
    ('Prijedor'),
    ('Zvornik'),
    ('Foca'),
    ('Laktaši'),
    ('Gradiška'),
    ('Nevesinje'),
    ('Bratunac'),
    ('Teslic');
");

            migrationBuilder.Sql(@"
                UPDATE Saloni
                SET GradId = 1
                WHERE Id = 1;

                UPDATE Saloni
                SET GradId = 5
                WHERE Id = 2;

                UPDATE Saloni
                SET GradId = 3
                WHERE Id = 3;

                UPDATE Saloni
                SET GradId = 3
                WHERE Id = 4; 
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saloni_Gradovi_GradId",
                table: "Saloni");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropIndex(
                name: "IX_Saloni_GradId",
                table: "Saloni");

            migrationBuilder.DropColumn(
                name: "GradId",
                table: "Saloni");
        }
    }
}
