using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Profilnaslika : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicture", "SecurityStamp" },
                values: new object[] { "4ffc09f7-af98-4d8f-aa72-8bcfee1e03bc", "AQAAAAIAAYagAAAAEGMK5vD0NV40JTxna/sxDqYrS59iENcp8tNFw9tNnzLQ80uZiCu81u5beL0mgjJouQ==", null, "d0f95f75-32d1-4c09-ad20-12af0d2a1d81" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c381c2eb-d72f-4979-b4fb-4ccb64124f5e", "AQAAAAIAAYagAAAAECnG1RAwRAItG3Rws3Kk/igyIX/5K1k5DZSXJYah5gDezsPMryTosBhpK8UdUdOw6A==", "2d552bf6-e119-42eb-8d40-94ef80fa7f22" });
        }
    }
}
