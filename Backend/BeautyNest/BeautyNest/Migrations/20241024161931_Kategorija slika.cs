using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class Kategorijaslika : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slika",
                table: "Kategorije",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
               table: "Kategorije",
               columns: new[] { "Naziv", "Slika" },
               values: new object[,]
               {
                    { "Frizerske usluge", "https://images.pexels.com/photos/3331486/pexels-photo-3331486.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { "Kozmetički tretmani", "https://images.pexels.com/photos/7691166/pexels-photo-7691166.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { "Manikir i pedikir", "https://images.pexels.com/photos/3997379/pexels-photo-3997379.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { "Wellness i spa", "https://images.pexels.com/photos/3757942/pexels-photo-3757942.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { "Masaže", "https://images.pexels.com/photos/3997982/pexels-photo-3997982.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { "Estetska medicina", "https://images.pexels.com/photos/12556700/pexels-photo-12556700.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { "Trajna šminka", "https://images.pexels.com/photos/9656928/pexels-photo-9656928.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { "Obrve i trepavice", "https://images.pexels.com/photos/5128267/pexels-photo-5128267.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" }
               });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Kategorije");
        }
    }
}
