using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class SalonSlikegalerija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalonSlike",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalonSlike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalonSlike_Saloni_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Saloni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalonSlike_SalonId",
                table: "SalonSlike",
                column: "SalonId");

            migrationBuilder.Sql(@"INSERT INTO SalonSlike (Url, SalonId)
                VALUES
                ('https://images.pexels.com/photos/2799605/pexels-photo-2799605.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 1),
                ('https://images.pexels.com/photos/853427/pexels-photo-853427.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 1),
                ('https://images.pexels.com/photos/3065209/pexels-photo-3065209.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 1),
                ('https://images.pexels.com/photos/7755220/pexels-photo-7755220.jpeg', 2),
                ('https://images.pexels.com/photos/7755250/pexels-photo-7755250.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 2),
                ('https://images.pexels.com/photos/7755224/pexels-photo-7755224.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 2),
                ('https://images.pexels.com/photos/7750091/pexels-photo-7750091.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 3),
                ('https://images.pexels.com/photos/7750100/pexels-photo-7750100.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 3),
                ('https://images.pexels.com/photos/7750119/pexels-photo-7750119.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 3),
                ('https://images.pexels.com/photos/5912297/pexels-photo-5912297.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 4),
                ('https://images.pexels.com/photos/3985338/pexels-photo-3985338.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1', 4)");



        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalonSlike");
        }
    }
}
